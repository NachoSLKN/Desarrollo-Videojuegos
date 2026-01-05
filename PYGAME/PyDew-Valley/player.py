import pygame
from settings import *
from support import *
from timer import Timer

class Player(pygame.sprite.Sprite):
    
    def __init__(self, pos, group, collision_sprites, tree_sprites, interaction, soil_layer, toggle_shop):
        super().__init__(group)
        #Este self import va arriba del todo de la función init para que cuando creemos el self image
        #del general SetUp, tengamos activop nuestro diccionario. 
        self.import_assets()
        self.status = 'down_idle'
        self.frame_index = 0 
        
        # SetUp General
        self.image = self.animations[self.status][self.frame_index]
        # self.image.fill('green')
        self.rect = self.image.get_rect(center = pos)
        #Copiamos el rectángulo y le cambiamos la dimensión con inflate mientras lo mantiene centrado, en el centro.
        self.z = LAYERS['main']
        
        # Atributos de movimiento
        self.direction = pygame.math.Vector2()
        self.pos = pygame.math.Vector2(self.rect.center)
        self.speed = 200
        
        # Colisiones
        
        self.collision_sprites = collision_sprites
        # self.hitbox = self.rect.copy().inflate((-126,-70)) #HorizontalAxis, VerticalAxis
        self.hitbox = pygame.Rect(0, 0, self.rect.width * 0.5, self.rect.height * 0.2)
        self.hitbox.midbottom = self.rect.midbottom

        
        # timers: atributo de tiempo para cierta acción
        self.timers= { #Diccionario
            'tool use': Timer(350, self.use_tools), #La clave es el nombre 'tool use': y el valor es el Timer en sí 
            'tool switch': Timer(200),
            'seed use': Timer(350, self.use_seed), 
            'seed switch': Timer(200)
            
        }
        
        # tools
        self.tools = ['hoe', 'water', 'axe']
        self.tool_index = 0 #Como nuestro index es 0, seleccionamos 'hoe' de la lista
        self.selected_tool = self.tools[self.tool_index]
        
        # seeds
        self.seeds = ['corn', 'tomato']
        self.seed_index = 0
        self.selected_seed = self.seeds[self.seed_index]
        
        # inventario: Si queremos añadir algo al jugador, tiene que tener un sistema donde almacenar datos
        self.item_inventory = {
            
            'wood':   10, # Madera de los árboles
            'apple':  10, # Manzanas de los árboles
            'corn':   10,  # Maíz de las plantas
            'tomato': 0 # Tomates de las plantas
        }
        
        self.seed_inventory = {
            'corn': 5,
            'tomato': 5
        }
        
        self.money = 200
        
        # interacción
        self.tree_sprites = tree_sprites
        self.interaction = interaction
        self.sleep = False
        self.soil_layer = soil_layer 
        self.toggle_shop = toggle_shop
        
        # sound
        
        self.watering = pygame.mixer.Sound('project/audio/water.mp3')
        self.watering.set_volume(0.2)
         
                
    def use_tools(self):
         if self.selected_tool == 'hoe':
             self.soil_layer.get_hit(self.target_pos)
         if self.selected_tool == 'axe':  
             for tree in self.tree_sprites.sprites():
                if tree.rect.collidepoint(self.target_pos):
                        tree.damage()
                 
         if self.selected_tool == 'water':
             self.soil_layer.water(self.target_pos)
             self.watering.play()
            
    def get_target_pos(self):  
        
        self.target_pos= self.rect.center + PLAYER_TOOL_OFFSET[self.status.split('_')[0]]     
                    
    def use_seed(self):
        if self.seed_inventory[self.selected_seed]>0:
             self.soil_layer.plant_seed(self.target_pos, self.selected_seed)         
             self.seed_inventory[self.selected_seed]  -= 1       
               
    def import_assets(self): 
        #Diccionario de animaciones: 
        #Tecla y valor de la tecla asociada para todos los estados que el jugador podría tomar.
        self.animations = {
    'up': [], 'down': [], 'left': [], 'right': [],
    'up_idle': [], 'down_idle': [], 'left_idle': [], 'right_idle': [],
    'up_hoe': [], 'down_hoe': [], 'left_hoe': [], 'right_hoe': [],
    'up_axe': [], 'down_axe': [], 'left_axe': [], 'right_axe': [],
    'up_water': [], 'down_water': [], 'left_water': [], 'right_water': []
}

   
        for animation in self.animations.keys():
            full_path = 'project/graphics/character/' + animation
            self.animations[animation] = import_folder(full_path)
        print(self.animations)    
   
    def animate(self, dt):
       self.frame_index += 4 * dt
       if self.frame_index >= len(self.animations[self.status]):
          self.frame_index = 0
           
       self.image = self.animations[self.status][int(self.frame_index)]

    def input(self):
        keys = pygame.key.get_pressed() #Devuelve una lista con todas las teclas potencialmente pulsables
        
        #Solo cuando el jugador no esté usando una herramienta
        #El jugador podrá moverse y usar una. No podemos usar una herramienta si ya la está usando,
        #Por eso este if 'not self.timers['tool use'].active:'
        
        if not self.timers['tool use'].active and not self.sleep:
            #directions
            if keys[pygame.K_UP]:
                #print('up')
                self.direction.y = -1 #Si presionamos arriba, la dirección de Y es -1
                self.status = 'up'
            elif keys[pygame.K_DOWN]:
                #print('down') 
                self.direction.y = 1 #Si presionamos abajo, la dirección es 1 positivo
                self.status = 'down'

            else:
                self.direction.y = 0 #Si no pulsamos nada, la dirección es 0
                
    
            if keys[pygame.K_RIGHT]:
                #print('right')
                self.direction.x = 1
                self.status = 'right'


            elif keys[pygame.K_LEFT]:
                #print('left')    
                self.direction.x = -1
                self.status = 'left'

            else:
                self.direction.x = 0    

            #print(self.direction) 
            
            
            #tool use
            if keys[pygame.K_SPACE]:
                #Timer for the tool use
                self.timers['tool use'].activate()
                self.direction = pygame.math.Vector2() #Hacemos que el jugador no se mueva cuando use una herramienta
                self.frame_index = 0 #Conseguimos empezar la animación desde el principio
 
            #Change tool
            if keys[pygame.K_q] and not self.timers['tool switch'].active: #El jugador solo puede cambiar de herramienta si presiona Q y el tool switch timer no está activado
                self.timers['tool switch'].activate()
                self.tool_index += 1
                self.tool_index = self.tool_index if self.tool_index < len(self.tools) else 0
                print(self.tool_index)
                self.selected_tool = self.tools[self.tool_index]
                
            #seed use
            if keys[pygame.K_LCTRL]:
                #Timer for the tool use
                self.timers['seed use'].activate()
                self.direction = pygame.math.Vector2() 
                self.frame_index = 0
                print('used seed') 

            #change seed
            if keys[pygame.K_e] and not self.timers['seed switch'].active: #El jugador solo puede cambiar de herramienta si presiona Q y el tool switch timer no está activado
                self.timers['seed switch'].activate()
                self.seed_index += 1
                self.seed_index = self.seed_index if self.seed_index < len(self.seeds) else 0
                print(self.seed_index)
                self.selected_seed = self.seeds[self.seed_index]
                print(self.selected_seed) 
                
            if keys[pygame.K_RETURN]: #si el jugador ha presionado el boton enter o no
                #self.toggle_shop()
                collided_interaction_sprite = pygame.sprite.spritecollide(self, self.interaction, False)
                if collided_interaction_sprite:
                    if collided_interaction_sprite[0].name == 'Trader':
                        self.toggle_shop()
                    else:
                        self.status = 'left_idle' #aplicamos idle izquierdo para interactuar cuando el jugador mira a la izquieda: la pos correcta    
                        self.sleep = True
                       
    def get_status(self): 
        
        # si el jugador no se está moviendo,
        if self.direction.magnitude() == 0:
        # #añadimos un _idle al estado
            self.status = self.status.split('_')[0] + '_idle'
            
            #idle
        if self.timers['tool use'].active:
            print('tool is being used')
            self.status = self.status.split('_')[0] + '_' + self.selected_tool
            
    def update_timers(self):
        for timer in self.timers.values():
            timer.update()    
                     
    def collision(self, direction):
     #Miramos todos los sprites dentro del collision sprite
     for sprite in self.collision_sprites.sprites():
         if hasattr(sprite,'hitbox'):
             
             # si el jugador se mueve a la derecha, cualquier colision tiene que ser a la izquierda del colisionable.
             # y viceversa
             if sprite.hitbox.colliderect(self.hitbox):
                 if direction == 'horizontal': # toda la lógica para las colisiones en el eje horitonzal
                     if self.direction.x > 0: #si el personaje se mueve hacia la derecha
                         self.hitbox.right = sprite.hitbox.left
                     if self.direction.x < 0: #si el personaje se mueve hacia la izquierda
                         self.hitbox.left = sprite.hitbox.right
                     self.rect.centerx = self.hitbox.centerx
                     self.pos.x = self.hitbox.centerx


                 if direction == 'vertical': # toda la lógica para las colisiones en el eje vertical
                     if self.direction.y > 0: #si el personaje se mueve hacia abajo
                         self.hitbox.bottom = sprite.hitbox.top
                     if self.direction.y < 0: #si el personaje se mueve hacia arriba
                         self.hitbox.top = sprite.hitbox.bottom
                     self.rect.centery = self.hitbox.centery
                     self.pos.y = self.hitbox.centery
                    
    def move(self, dt):
        
        #Normalizando Vector: Pitágoras / Velocidad constante
        if self.direction.magnitude() > 0:
           self.direction = self.direction.normalize() 
       
       # Horizontal Movement
       
        #Solo podemos normalizar un vector si este tiene una longitud, si apunta al 0,0 no se puede hacer
        #print(self.direction)
        self.pos.x += self.direction.x * self.speed*dt
        #Usamos round para que pygame no trunke los resultados 1.9 = 1 cuando debería ser 2. Tendríamos comportamiento incorrecto.
        self.hitbox.centerx = round(self.pos.x)
        self.rect.centerx = self.hitbox.centerx
        self.collision('horizontal')
        
       # Vertical Movement 
        self.pos.y += self.direction.y * self.speed*dt
        self.hitbox.centery = round(self.pos.y)
        self.rect.centery = self.hitbox.centery
        self.collision('vertical')
       
    def update(self, dt): 
        #DeltaTime nos ayuda a mover todo de forma independiente al frameRate
        self.input()
        self.get_status()
        self.update_timers()
        self.get_target_pos()
        
        self.move(dt)
        self.animate(dt)