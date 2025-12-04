import pygame, sys #Necesitamos sys para cerrar el juego
from settings import * 
from level import Level

class Game: #Creación de la clase Game
    def __init__(self): #Método init
        pygame.init() #Iniciamos PyGame 
        self.screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT)) #Creación de display surface
        pygame.display.set_caption('Pydew Valley') #Cambio de nombre de ventana
        self.clock = pygame.time.Clock() #Creación de un clock
        self.level=Level()
        
    def run (self): #Método Run. La mayoría del juego se ejecutará aquí
        while True:
            for event in pygame.event.get(): #Loop para ver si cerramos el juego
                if event.type == pygame.QUIT:
                    pygame.quit()
                    sys.exit() #Cerramos juego
                    
            dt=self.clock.tick()/1000 #Calculamos Delta Time
            self.level.run(dt)
            pygame.display.update() #Actualizamos el juego

#Checkeamos si estamos en el archivo main
if __name__== '__main__':
    game=Game() #Creamos un objeto con la clase
    game.run() #Ejecutamos el método Run
    
    