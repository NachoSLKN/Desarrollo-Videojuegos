import pygame
import sys
from settings import *
from ui import UI
from resource_path import resource_path


class NameInput:

    def __init__(self):

        self.display = pygame.display.get_surface()

        self.font_title = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            60
        )

        self.font = pygame.font.Font(
            resource_path("project/font/LycheeSoda.ttf"),
            40
        )

        self.player_name = ""
        self.max_characters = 12

    def input(self):

        for event in pygame.event.get():

            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

            if event.type == pygame.KEYDOWN:

                if event.key == pygame.K_ESCAPE:
                    return "menu"

                if event.key == pygame.K_BACKSPACE:
                    self.player_name = self.player_name[:-1]

                elif event.key == pygame.K_RETURN:

                    if self.player_name.strip():
                        return self.player_name.strip()

                elif len(self.player_name) < self.max_characters:

                    if event.unicode.isprintable():
                        self.player_name += event.unicode

        return None

    def draw(self):

        self.display.fill((25, 30, 40))

        panel = pygame.Rect(
            SCREEN_WIDTH // 2 - 350,
            SCREEN_HEIGHT // 2 - 180,
            700,
            360
        )

        UI.draw_panel(self.display, panel)

        title = self.font_title.render(
            "¿Cómo te llamas?",
            True,
            "black"
        )

        self.display.blit(
            title,
            (
                SCREEN_WIDTH // 2 - title.get_width() // 2,
                panel.top + 45
            )
        )

        name_text = self.player_name if self.player_name else "Escribe tu nombre"

        color = "black" if self.player_name else (100, 100, 100)

        name_surface = self.font.render(
            name_text,
            True,
            color
        )

        input_rect = pygame.Rect(
            panel.left + 90,
            panel.top + 145,
            panel.width - 180,
            70
        )

        pygame.draw.rect(
            self.display,
            (245, 225, 190),
            input_rect,
            border_radius=8
        )

        pygame.draw.rect(
            self.display,
            (90, 60, 30),
            input_rect,
            3,
            border_radius=8
        )

        self.display.blit(
            name_surface,
            (
                input_rect.centerx - name_surface.get_width() // 2,
                input_rect.centery - name_surface.get_height() // 2
            )
        )

        help_text = self.font.render(
            "Enter para continuar · Esc para volver",
            True,
            "black"
        )

        help_text = pygame.transform.scale(
            help_text,
            (
                int(help_text.get_width() * 0.7),
                int(help_text.get_height() * 0.7)
            )
        )

        self.display.blit(
            help_text,
            (
                SCREEN_WIDTH // 2 - help_text.get_width() // 2,
                panel.bottom - 75
            )
        )