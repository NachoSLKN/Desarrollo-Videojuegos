import pygame

class UI:

    @staticmethod
    def draw_panel(surface, rect):

        # Fondo madera
        pygame.draw.rect(
            surface,
            (226, 201, 161),
            rect,
            border_radius=12
        )

        # Borde oscuro
        pygame.draw.rect(
            surface,
            (88, 61, 37),
            rect,
            width=4,
            border_radius=12
        )