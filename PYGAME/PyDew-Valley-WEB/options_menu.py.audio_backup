import pygame
import sys

from settings import *
from ui import UI
from resource_path import resource_path


class OptionsMenu:

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

        # Estado de las opciones
        self.music_enabled = True
        self.effects_enabled = True
        self.fullscreen = False

        self.selected = 0

        self.update_options()

    def update_options(self):

        music_text = "ON" if self.music_enabled else "OFF"
        effects_text = "ON" if self.effects_enabled else "OFF"
        fullscreen_text = "ON" if self.fullscreen else "OFF"

        self.options = [
            f"Música: {music_text}",
            f"Efectos: {effects_text}",
            f"Pantalla completa: {fullscreen_text}",
            "Volver"
        ]

    def toggle_music(self):

        self.music_enabled = not self.music_enabled

        if self.music_enabled:
            pygame.mixer.music.unpause()
        else:
            pygame.mixer.music.pause()

        self.update_options()

    def toggle_effects(self):

        self.effects_enabled = not self.effects_enabled

        if self.effects_enabled:
            pygame.mixer.unpause()
        else:
            pygame.mixer.pause()

        self.update_options()

    def toggle_fullscreen(self):

        self.fullscreen = not self.fullscreen

        if self.fullscreen:
            self.display = pygame.display.set_mode(
                (SCREEN_WIDTH, SCREEN_HEIGHT),
                pygame.FULLSCREEN
            )
        else:
            self.display = pygame.display.set_mode(
                (SCREEN_WIDTH, SCREEN_HEIGHT)
            )

        self.update_options()

    def input(self):

        for event in pygame.event.get():

            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

            if event.type == pygame.KEYDOWN:

                if event.key == pygame.K_UP:
                    self.selected = (
                        self.selected - 1
                    ) % len(self.options)

                elif event.key == pygame.K_DOWN:
                    self.selected = (
                        self.selected + 1
                    ) % len(self.options)

                elif event.key == pygame.K_RETURN:

                    if self.selected == 0:
                        self.toggle_music()

                    elif self.selected == 1:
                        self.toggle_effects()

                    elif self.selected == 2:
                        self.toggle_fullscreen()

                    elif self.selected == 3:
                        return "Volver"

                elif event.key == pygame.K_ESCAPE:
                    return "Volver"

        return None

    def draw(self):

        # Recuperamos la superficie por si ha cambiado
        # entre ventana y pantalla completa.
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
            "Opciones",
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

            surf = self.font.render(
                text,
                True,
                color
            )

            self.display.blit(
                surf,
                (
                    SCREEN_WIDTH // 2 - surf.get_width() // 2,
                    240 + i * 70
                )
            )