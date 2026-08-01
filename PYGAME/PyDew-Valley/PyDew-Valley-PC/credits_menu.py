import pygame
import sys

from settings import *
from ui import UI
from resource_path import resource_path


class CreditsMenu:

    def __init__(self):

        self.display = pygame.display.get_surface()

        self.font_title = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            60
        )

        self.font = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            34
        )

    def input(self):

        for event in pygame.event.get():

            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

            if event.type == pygame.KEYDOWN:
                return "volver"

        return None

    def draw(self):

        self.display.fill((25,30,40))

        panel = pygame.Rect(
            SCREEN_WIDTH//2-320,
            90,
            640,
            520
        )

        UI.draw_panel(self.display,panel)

        textos = [

            "PyDew Valley",
            "",
            "Programación",
            "Nacho SLKN",
            "",
            "Arte",
            "Cup Nooble",
            "",
            "Pulsa cualquier tecla"
        ]

        y = 130

        for texto in textos:

            fuente = self.font_title if texto=="PyDew Valley" else self.font

            surf = fuente.render(
                texto,
                True,
                "black"
            )

            self.display.blit(
                surf,
                (
                    SCREEN_WIDTH//2-surf.get_width()//2,
                    y
                )
            )

            y += 50