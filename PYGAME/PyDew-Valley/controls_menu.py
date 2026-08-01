import pygame
import sys

from settings import *
from ui import UI
from resource_path import resource_path


class ControlsMenu:

    def __init__(self):

        self.display = pygame.display.get_surface()

        self.font_title = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            60
        )

        self.font = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            32
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

        self.display = pygame.display.get_surface()
        self.display.fill((25, 30, 40))

        panel = pygame.Rect(
            SCREEN_WIDTH // 2 - 350,
            70,
            700,
            580
        )

        UI.draw_panel(self.display, panel)

        title = self.font_title.render(
            "Controles",
            True,
            "black"
        )

        self.display.blit(
            title,
            (
                SCREEN_WIDTH // 2 - title.get_width() // 2,
                100
            )
        )

        controls = [
            "Flechas - Moverse",
            "Espacio - Usar herramienta",
            "Q - Cambiar herramienta",
            "Ctrl izquierdo - Plantar semilla",
            "E - Cambiar semilla",
            "Enter - Interactuar / Dormir / Tienda",
            "P - Pausa",
            "F5 - Guardar partida",
            "",
            "Pulsa cualquier tecla para volver"
        ]

        y = 190

        for text in controls:

            surface = self.font.render(
                text,
                True,
                "black"
            )

            self.display.blit(
                surface,
                (
                    SCREEN_WIDTH // 2 - surface.get_width() // 2,
                    y
                )
            )

            y += 43