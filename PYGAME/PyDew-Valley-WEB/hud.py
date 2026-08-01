
import pygame
from settings import *
from ui import UI
from resource_path import resource_path

class HUD:

    def __init__(self, player, player_name):

        self.player = player
        self.display_surface = pygame.display.get_surface()

        self.font = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            28
        )

        self.day = 1
        self.player_name = player_name

        #Retrato inicial
        self.portrait_size = 176
        self.portrait = pygame.transform.scale(
            self.player.animations['down_idle'][0],
            (self.portrait_size, self.portrait_size)
        )



        self.portrait = pygame.transform.scale(
        self.player.animations['down_idle'][0],
        (48, 48)
)

    def draw(self):

        self.update_portrait()

        panel = pygame.Rect(20,20,340,140)
          
        UI.draw_panel(self.display_surface,panel)

        self.display_surface.blit(self.portrait, (30, 30))

        name = self.font.render(
            self.player_name,
            False,
            "black"
        )

        day = self.font.render(
            f"Dia {self.day}",
            False,
            "black"
        )

        self.display_surface.blit(name, (195, 35))
        self.display_surface.blit(day, (195, 75))

    def update_portrait(self):

        self.portrait = pygame.transform.scale(
            self.player.image,
            (self.portrait_size, self.portrait_size)
        )

       