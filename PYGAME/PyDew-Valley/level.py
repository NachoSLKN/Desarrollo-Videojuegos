import pygame
from settings import *
from player import Player
from overlay import Overlay
from sprites import Generic, Water, WildFlower, Tree
from pytmx.util_pygame import load_pygame
from support import *  

#Esta clase ayuda a mantener el proyecto limpio y organizado
class Level:
    def __init__(self):
        self.display_surface = pygame.display.get_surface() #Este display surface es el mismo que el del main.py
        #Permite que se dibuje directamente en la pantalla que se visualiza al jugador
    
        # sprite groups
        self.all_sprites = CameraGroup()
    
        self.setup()
        self.overlay = Overlay(self.player)
    
    def setup(self):
        
        
        tmx_data = load_pygame('project/data/map.tmx') #Cargamos el mapa tmx
        
        # house
        for layer in ['HouseFurnitureBottom', 'HouseFloor']:
             for x, y, surf in tmx_data.get_layer_by_name(layer).tiles():
                Generic((x*TITLE_SIZE, y * TITLE_SIZE), surf, self.all_sprites, LAYERS['house bottom'])

        for layer in ['HouseWalls', 'HouseFurnitureTop']:
             for x, y, surf in tmx_data.get_layer_by_name(layer).tiles():
                Generic((x*TITLE_SIZE, y * TITLE_SIZE), surf, self.all_sprites)
        
        
        # fence
        for x, y, surf in tmx_data.get_layer_by_name('Fence').tiles():
            Generic(
                (x * TITLE_SIZE, y * TITLE_SIZE),
                surf,
                self.all_sprites,
                LAYERS['main']   
    )

        # water
        water_frames = import_folder('project/graphics/water')
        for x, y, surf in tmx_data.get_layer_by_name('Water').tiles():
            Water((x * TITLE_SIZE, y * TITLE_SIZE),water_frames,self.all_sprites)
        
        
        # trees
        for obj in tmx_data.get_layer_by_name('Trees'):
            Tree((obj.x, obj.y), obj.image, self.all_sprites, obj.name)
        
        
        # wildflowers
        for obj in tmx_data.get_layer_by_name('Decoration'):
             WildFlower((obj.x, obj.y),obj.image,self.all_sprites)
        
        
        
        self.player = Player((640, 360), self.all_sprites) # Instancia de la clase Player
                             #x,y        #group   
        Generic( 
            pos = (0,0), 
            surf = pygame.image.load('project/graphics/world/ground.png').convert_alpha(), 
            groups = self.all_sprites, 
            z = LAYERS['ground'])

    def run (self, dt):
        #print ('Run Game')
        self.display_surface.fill('black')
        self.all_sprites.custom_draw(self.player)
        #self.all_sprites.draw(self.display_surface)
        self.all_sprites.update(dt)
        
        self.overlay.display()
    
class CameraGroup(pygame.sprite.Group):
    def __init__(self):
        super().__init__()
        self.display_surface = pygame.display.get_surface() #Así esta clase podrá dibujar directamente en la superficie del display
        self.offset = pygame.math.Vector2()
        
    def custom_draw(self, player): #Nos ayuda a pintar el player pese a ser instanciado antes que el mundo 
        self.offset.x = player.rect.centerx - SCREEN_WIDTH / 2
        self.offset.y = player.rect.centery - SCREEN_HEIGHT / 2
        
        
        for layer in LAYERS.values():
            for sprite in sorted(self.sprites(), key = lambda sprite: sprite.rect.centery):
                if sprite.z == layer:
                 offset_rect = sprite.rect.copy()
                 offset_rect.center -= self.offset
                 self.display_surface.blit(sprite.image, offset_rect)
               