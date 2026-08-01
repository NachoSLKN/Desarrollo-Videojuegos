import pygame
import sys
from settings import *
from ui import UI
from resource_path import resource_path

class MainMenu:

    def __init__(self, options=None, title="Pydew Valley"):

        self.display = pygame.display.get_surface()

        self.font_title = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            60
        )

        self.font = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            40
        )

        self.options = options or [
            "Nueva partida",
            "Opciones",
            "Créditos",
            "Salir"
        ]

        self.title = title

        self.selected = 0

    def input(self):

        for event in pygame.event.get():

            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

            if event.type == pygame.KEYDOWN:

                if event.key == pygame.K_UP:
                    self.selected = (self.selected-1) % len(self.options)

                if event.key == pygame.K_DOWN:
                    self.selected = (self.selected+1) % len(self.options)

                if event.key == pygame.K_RETURN:
                    return self.options[self.selected]

        return None

    def draw(self):

        self.display = pygame.display.get_surface()

        self.display.fill((25, 30, 40))

        panel = pygame.Rect(
            SCREEN_WIDTH // 2 - 300,
            100,
            600,
            500
        )

        UI.draw_panel(self.display, panel)

        title = self.font_title.render(
            self.title,
            True,
            "black"
        )

        self.display.blit(
            title,
            (
                SCREEN_WIDTH // 2 - title.get_width() // 2,
                130
            )
        )

        for i, text in enumerate(self.options):

            color = (
                (0, 0, 0)
                if i == self.selected
                else (80, 80, 80)
            )

            option_surface = self.font.render(
                text,
                True,
                color
            )

            self.display.blit(
                option_surface,
                (
                    SCREEN_WIDTH // 2 - option_surface.get_width() // 2,
                    240 + i * 70
                )
            )

        # Versión, fuera del for
        version = self.font.render(
            "v1.0",
            True,
            (80, 80, 80)
        )

        self.display.blit(
            version,
            (20, SCREEN_HEIGHT - 50)
        )