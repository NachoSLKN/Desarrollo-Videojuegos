import pygame
from save_manager import SaveManager
from settings import *
from player import Player
from overlay import Overlay
from hud import HUD
from sprites import (
    Generic,
    CollisionTile,
    Water,
    WildFlower,
    Tree,
    Interaction,
    Particles
)
from pytmx.util_pygame import load_pygame
from support import *  
from transition import Transition
from soil import SoilLayer
from sky import Rain, Sky
from random import randint
from menu import Menu
from resource_path import resource_path

class Level:
    #Esta clase ayuda a mantener el proyecto limpio y organizado
    def __init__(self, player_name="Nacho"):
        self.player_name= player_name
        self.display_surface = pygame.display.get_surface() #Este display surface es el mismo que el del main.py
        #Permite que se dibuje directamente en la pantalla que se visualiza al jugador
    
        # sprite groups
        self.all_sprites = CameraGroup()
        # necesitamos saber que sprites son colisionables. Todos los sprites con los que se puedan colisionar estarán aquí almacenados.
        self.collision_sprites = pygame.sprite.Group()
        self.tree_sprites=pygame.sprite.Group()
        self.Interaction_sprites=pygame.sprite.Group()
    
        self.soil_layer = SoilLayer(self.all_sprites, self.collision_sprites)
        self.setup() #Crea al player
        self.overlay = Overlay(self.player)
        self.hud = HUD(self.player, self.player_name)
        self.transition = Transition(self.reset, self.player)
    
        # sky
        
        self.rain = Rain(self.all_sprites)
        self.raining = randint(0,10) > 2
        self.soil_layer.raining = self.raining
        self.sky = Sky()
        
        # shop
        self.menu = Menu(self.player, self.toggle_shop)
        self.shop_active = False
        
        #music
        self.success = pygame.mixer.Sound(resource_path('project/audio/success.wav'))
        self.success.set_volume(0.3)
        self.music = pygame.mixer.Sound(resource_path('project/audio/music.mp3'))
        self.music.play(loops = -1)
        self.music.set_volume(0.1)
        
    def setup(self):
        
        
        tmx_data = load_pygame(resource_path('project/data/map.tmx')) #Cargamos el mapa tmx
        
        # house
        for layer in ['HouseFurnitureBottom', 'HouseFloor']:
             for x, y, surf in tmx_data.get_layer_by_name(layer).tiles():
                Generic((x*TITLE_SIZE, y * TITLE_SIZE), surf, self.all_sprites, LAYERS['house bottom'])

        for layer in ['HouseWalls', 'HouseFurnitureTop']:
            for x, y, surf in tmx_data.get_layer_by_name(layer).tiles():
                Generic(
                    (x * TITLE_SIZE, y * TITLE_SIZE),
                    surf,
                    self.all_sprites,
                    LAYERS['house top']
                )
        
        
        # fence
        for x, y, surf in tmx_data.get_layer_by_name('Fence').tiles():
            Generic(
                (x * TITLE_SIZE, y * TITLE_SIZE),
                surf,
                [self.all_sprites, self.collision_sprites],
                LAYERS['main']   
    )

        # water
        water_frames = import_folder('project/graphics/water')
        for x, y, surf in tmx_data.get_layer_by_name('Water').tiles():
            Water((x * TITLE_SIZE, y * TITLE_SIZE),water_frames,self.all_sprites)
        
        
        # trees
        for obj in tmx_data.get_layer_by_name('Trees'):
            Tree(
                pos= (obj.x, obj.y), 
                surf= obj.image, 
                groups= [self.all_sprites,self.collision_sprites, self.tree_sprites ], 
                name= obj.name,
                player_add = self.player_add)
        
        
        # wildflowers
        for obj in tmx_data.get_layer_by_name('Decoration'):
             WildFlower((obj.x, obj.y),obj.image, [self.all_sprites, self.collision_sprites])
        
        # collisions tiles
        for x, y, surf in tmx_data.get_layer_by_name('Collision').tiles():
            CollisionTile(
                (x * TITLE_SIZE, y * TITLE_SIZE),
                pygame.Surface((TITLE_SIZE, TITLE_SIZE)),
                self.collision_sprites
            )

        
        # player
        for obj in tmx_data.get_layer_by_name('Player'):
            if obj.name == 'Start':
                self.player = Player(
                    pos = (obj.x, obj.y), 
                    group = self.all_sprites, 
                    collision_sprites = self.collision_sprites,
                    tree_sprites = self.tree_sprites,
                    interaction = self.Interaction_sprites, # Instancia de la clase Player
                    soil_layer = self.soil_layer,
                    toggle_shop = self.toggle_shop)                #x,y        #group   
                    
       
            if obj.name=='Bed':
                Interaction((obj.x, obj.y),(obj.width, obj.height),self.Interaction_sprites,obj.name)

            if obj.name == 'Trader':
                Interaction((obj.x, obj.y),(obj.width, obj.height),self.Interaction_sprites,obj.name)


        Generic( 
            pos = (0,0), 
            surf = pygame.image.load(resource_path('project/graphics/world/ground.png')).convert_alpha(), 
            groups = self.all_sprites, 
            z = LAYERS['ground'])

    def player_add(self, item):
        self.player.item_inventory[item] +=1
        self.success.play()

    def toggle_shop(self):
        
        self.shop_active = not self.shop_active


    #def reset(self):
        
        # apples on the trees
        #for tree in self.tree_sprites.sprites():
            #for apple in tree.apple_sprites.sprites():
             #   apple.kill() # destruimos todas las manzanas
            #tree.create_fruit()   # creamos nuevas

    #def reset(self):
        # soil
     #   self.soil_layer.remove_water()

        # apples on the trees
      #  for tree in self.tree_sprites.sprites():
       #     if isinstance(tree, Tree):
        #        for apple in tree.apple_sprites.sprites():
         #           apple.kill()
          #      tree.create_fruit()



    def reset(self):


            self.hud.day += 1
            self.save_game()

            self.soil_layer.update_plants()


            # soil
            self.soil_layer.remove_water()
            # randomize rain
            self.raining = randint(0,10) > 5
            self.soil_layer.raining = self.raining 
            if self.raining:
                self.soil_layer.water_all()

            # eliminar stumps
            for sprite in self.tree_sprites.sprites():
                sprite.kill()

            # volver a crear árboles desde el TMX
            tmx_data = load_pygame(resource_path('project/data/map.tmx'))
            for obj in tmx_data.get_layer_by_name('Trees'):
                Tree(
                    pos=(obj.x, obj.y),
                    surf=obj.image,
                    groups=[self.all_sprites, self.collision_sprites, self.tree_sprites],
                    name=obj.name,
                    player_add=self.player_add
                )
                
                
            #Sky
            self.sky.start_color = [255,255,255]  

    def plant_collision(self):
        if self.soil_layer.plant_sprites:
            for plant in self.soil_layer.plant_sprites.sprites():
                if plant.harvestable and plant.rect.colliderect(self.player.hitbox):
                    self.player_add(plant.plant_type)
                    plant.kill()
                    Particles(plant.rect.topleft, plant.image, self.all_sprites, z = LAYERS['main'])
                    self.soil_layer.grid[plant.rect.centery//TITLE_SIZE][plant.rect.centerx//TITLE_SIZE].remove('P')

    def run (self, dt):
        
        
        # logica de dibujado
        
        # print ('Run Game')
        self.display_surface.fill('black')
        self.all_sprites.custom_draw(self.player)
        # self.all_sprites.draw(self.display_surface)
        
        #actualizaciones
        if self.shop_active:
            self.menu.update()
        else:    
            self.all_sprites.update(dt)
            self.plant_collision()
        
        
        #Sección del clima
        
            # HUD
        self.overlay.display()
        self.hud.draw()

        # lluvia
        if self.raining and not self.shop_active:
            self.rain.update()

        # daytime
        self.sky.display(dt)

        # transition overlay
        if self.player.sleep:
            self.transition.play()
        
        #print(self.shop_active)

        keys = pygame.key.get_pressed()

        if keys[pygame.K_F5]:
            self.save_game()
                
    def save_game(self):

        data = {
            "player_name": self.player_name,
            "day": self.hud.day,
            "player_x": self.player.rect.centerx,
            "player_y": self.player.rect.centery
        }

        SaveManager.save_game(data)

        print("Partida guardada")

    
class CameraGroup(pygame.sprite.Group):
    def __init__(self):
        super().__init__()
        self.display_surface = pygame.display.get_surface()
        self.offset = pygame.math.Vector2()

    def custom_draw(self, player):
        self.offset.x = player.rect.centerx - SCREEN_WIDTH / 2
        self.offset.y = player.rect.centery - SCREEN_HEIGHT / 2

        for layer in LAYERS.values():
            for sprite in sorted(self.sprites(), key=lambda sprite: sprite.rect.centery):
                if sprite.z == layer:
                    offset_rect = sprite.rect.copy()
                    offset_rect.center -= self.offset
                    self.display_surface.blit(sprite.image, offset_rect)

        # DEBUG DEL JUGADOR: se dibuja una sola vez y en su posición real.
        # Comenta estas líneas cuando termines de probar las colisiones.
        # DEBUG DEL JUGADOR
        if DEBUG:

            player_rect = player.rect.copy()
            player_rect.center -= self.offset
            pygame.draw.rect(
                self.display_surface,
                'red',
                player_rect,
                3
            )

            player_hitbox = player.hitbox.copy()
            player_hitbox.center -= self.offset
            pygame.draw.rect(
                self.display_surface,
                'green',
                player_hitbox,
                3
            )

            if hasattr(player, 'target_pos'):
                target_pos = player.target_pos - self.offset
                pygame.draw.circle(
                    self.display_surface,
                    'blue',
                    target_pos,
                    5
                )