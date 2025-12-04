import pygame
from settings import *
from support import *
from timer import Timer

class Player(pygame.sprite.Sprite):
    
    def __init__(self, pos, group, collision_sprites):
        super().__init__(group)
        #Este self import va arriba del todo de la función init para que cuando creemos el self image
        #del general SetUp, tengamos activop nuestro diccionario. 
        self.import_assets()
        self.status = 'down_idle'
        self.frame_index = 0 
        
        # General SetUp
        self.image = self.animations[self.status][self.frame_index]
        #self.image.fill('green')
        self.rect = self.image.get_rect(center = pos)
        #Copiamos el rectángulo y le cambiamos la dimensión con inflate mientras lo mantiene centrado, en el centro.
        self.z = LAYERS['main']
        
        # Movements Attributes
        self.direction = pygame.math.Vector2()
        self.pos = pygame.math.Vector2(self.rect.center)
        self.speed = 200
        
        # collision
        
        self.collision_sprites = collision_sprites
        # self.hitbox = self.rect.copy().inflate((-126,-70)) #HorizontalAxis, VerticalAxis
        self.hitbox = pygame.Rect(0, 0, self.rect.width * 0.5, self.rect.height * 0.2)
        self.hitbox.midbottom = self.rect.midbottom

        
        #timers: atributo de tiempo para cierta acción
        self.timers= { #Diccionario
            'tool use': Timer(350, self.use_tools), #La clave es el nombre 'tool use': y el valor es el Timer en sí 
            'tool switch': Timer(200),
            'seed use': Timer(350, self.use_seed), 
            'seed switch': Timer(200)
            
        }
        
        #tools
        self.tools = ['hoe', 'water', 'axe']
        self.tool_index = 0 #Como nuestro index es 0, seleccionamos 'hoe' de la lista
        self.selected_tool = self.tools[self.tool_index]
        
        #seeds
        self.seeds = ['corn', 'tomato']
        self.seed_index = 0
        self.selected_seed = self.seeds[self.seed_index]
                
    def use_tools(self):
         pass
         #print(self.selected_tool)   
            
    def use_seed(self):
        pass         
               
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
        
        if not self.timers['tool use'].active:
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
       
    def update(self, dt): #DeltaTime nos ayuda a mover todo de forma independiente al frameRate
        self.input()
        self.get_status()
        self.update_timers()
        self.move(dt)
        self.animate(dt)