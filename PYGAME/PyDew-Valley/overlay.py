import pygame
from settings import *


class Overlay:
    def __init__(self, player): #Player como parámetro porque el overlay necesita saber que ha seleccionado el player
        
        # general setup
        self.display_surface = pygame.display.get_surface()
        self.player = player

        # imports
        overlay_path = 'project/graphics/overlay/'

        scale_factor = 4  # <<< ICONOS 4x MÁS GRANDES

        # tools
        self.tools_surf = {}
        for tool in player.tools:
            surf = pygame.image.load(f'{overlay_path}{tool}.png').convert_alpha()
            w, h = surf.get_width(), surf.get_height()
            surf = pygame.transform.scale(surf, (w * scale_factor, h * scale_factor))
            self.tools_surf[tool] = surf

        # seeds
        self.seeds_surf = {}
        for seed in player.seeds:
            surf = pygame.image.load(f'{overlay_path}{seed}.png').convert_alpha()
            w, h = surf.get_width(), surf.get_height()
            surf = pygame.transform.scale(surf, (w * scale_factor, h * scale_factor))
            self.seeds_surf[seed] = surf

        print(self.tools_surf)
        print(self.seeds_surf)

    def display(self):
        
        #tools
        tool_surf = self.tools_surf[self.player.selected_tool]
        tool_rect = tool_surf.get_rect(midbottom = OVERLAY_POSITIONS['tool'])
        self.display_surface.blit(tool_surf, tool_rect)
        
        #seeds
        seed_surf = self.seeds_surf[self.player.selected_seed]
        seed_rect = seed_surf.get_rect(midbottom = OVERLAY_POSITIONS['seed'])
        self.display_surface.blit(seed_surf, seed_rect)
