# /// script
# dependencies = [
#     "pygame-ce",
#     "pytmx",
# ]
# ///

import pygame
import sys

from settings import *
from level import Level
from main_menu import MainMenu
from name_input import NameInput
from options_menu import OptionsMenu
from save_manager import SaveManager
from credits_menu import CreditsMenu
from resource_path import resource_path
from controls_menu import ControlsMenu
import asyncio


class Game:

    def __init__(self):

        pygame.init()

        self.screen = pygame.display.set_mode(
            (SCREEN_WIDTH, SCREEN_HEIGHT)
        )

        pygame.display.set_caption("Pydew Valley")

        icon = pygame.image.load(
            resource_path("project/graphics/overlay/axe.png")
        ).convert_alpha()

        pygame.display.set_icon(icon)

        self.clock = pygame.time.Clock()

        self.level = None

        # Menú principal
        self.menu = MainMenu(
            [
                "Nueva partida",
                "Opciones",
                "Controles",
                "Créditos",
                "Salir"
            ],
            "Pydew Valley"
        )

        # Menú de pausa
        self.pause_menu = MainMenu(
            [
                "Continuar",
                "Guardar partida",
                "Salir al menú"
            ],
            "Pausa"
        )

        self.name_input = NameInput()
        self.options_menu = OptionsMenu()
        self.credits = CreditsMenu()
        self.controls = ControlsMenu()

        self.state = "menu"
        self.paused = False

    async def run(self):

        while True:

            dt = self.clock.tick() / 1000

            # -------------------------
            # MENÚ PRINCIPAL
            # -------------------------
            if self.state == "menu":

                # Mostrar Continuar solo si existe guardado
                if SaveManager.save_exists():

                    self.menu.options = [
                        "Continuar",
                        "Nueva partida",
                        "Opciones",
                        "Controles",
                        "Créditos",
                        "Salir"
                    ]

                else:

                    self.menu.options = [
                        "Nueva partida",
                        "Opciones",
                        "Controles",
                        "Créditos",
                        "Salir"
                    ]

                # Evitar que selected quede fuera de la lista
                self.menu.selected = min(
                    self.menu.selected,
                    len(self.menu.options) - 1
                )

                action = self.menu.input()

                if action == "Continuar":

                    data = SaveManager.load_game()

                    print(">>> ANTES DE CREAR LEVEL (CONTINUAR) <<<")

                    self.level = Level(
                        data["player_name"]
                    )

                    print(">>> LEVEL CREADO (CONTINUAR) <<<")

                    self.level.player.rect.center = (
                        data["player_x"],
                        data["player_y"]
                    )

                    # Sincronizar posición interna del jugador
                    self.level.player.hitbox.midbottom = (
                        self.level.player.rect.midbottom
                    )

                    self.level.player.pos = pygame.math.Vector2(
                        self.level.player.hitbox.center
                    )

                    self.level.hud.day = data["day"]

                    self.state = "game"

                elif action == "Nueva partida":

                    self.name_input.player_name = ""
                    self.state = "name_input"

                elif action == "Opciones":

                    self.state = "options"

                elif action == "Controles":

                    self.state = "controls"

                elif action == "Créditos":

                    self.state = "credits"

                elif action == "Salir":

                    pygame.quit()
                    sys.exit()

                self.menu.draw()

            # -------------------------
            # OPCIONES
            # -------------------------
            elif self.state == "options":

                action = self.options_menu.input()

                if action == "Volver":
                    self.state = "menu"

                self.options_menu.draw()

            # -------------------------
            # CONTROLES
            # -------------------------
            elif self.state == "controls":

                action = self.controls.input()

                if action == "volver":
                    self.state = "menu"

                self.controls.draw()

            # -------------------------
            # CRÉDITOS
            # -------------------------
            elif self.state == "credits":

                action = self.credits.input()

                if action == "volver":
                    self.state = "menu"

                self.credits.draw()

            # -------------------------
            # INTRODUCIR NOMBRE
            # -------------------------
           # -------------------------
# INTRODUCIR NOMBRE
# -------------------------
            elif self.state == "name_input":

                result = self.name_input.input()

                if result == "menu":

                    self.state = "menu"

                elif result:

                    print(">>> ANTES DE CREAR LEVEL (NUEVA PARTIDA) <<<")

                    self.level = Level(result)

                    print(">>> LEVEL CREADO (NUEVA PARTIDA) <<<")

                    self.state = "game"

                self.name_input.draw()

            # -------------------------
            # JUEGO
            # -------------------------
            elif self.state == "game":

                if self.paused:

                    action = self.pause_menu.input()

                    if action == "Continuar":

                        self.paused = False

                    elif action == "Guardar partida":

                        self.level.save_game()

                    elif action == "Salir al menú":

                        self.paused = False
                        self.state = "menu"

                    self.pause_menu.draw()

                else:

                    for event in pygame.event.get():

                        if event.type == pygame.QUIT:
                            pygame.quit()
                            sys.exit()

                        if event.type == pygame.KEYDOWN:

                            if event.key == pygame.K_p:
                                self.paused = True

                            elif event.key == pygame.K_F5:
                                self.level.save_game()

                    self.level.run(dt)

            pygame.display.update()
            await asyncio.sleep(0)


async def main():

    game = Game()
    await game.run()


asyncio.run(main())