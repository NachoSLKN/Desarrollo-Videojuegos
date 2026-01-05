from os import walk # modulo que nos permite andar a través de carpetas
import pygame

def import_folder(path): #Funcion que importa el contenido de una carpeta y lo guarda en superficies
    surface_list = []
    
    for _, __, img_files in walk(path): #Devuelve una lista con todos los contenidos de las carpetas.
        for image in img_files:
            full_path = path + '/' + image           
            image_surf = pygame.image.load(full_path).convert_alpha()
            surface_list.append(image_surf)
    return surface_list 

def import_folder_dict(path):
    surface_dict = {}
    
    for _, __, img_files in walk(path): #Devuelve una lista con todos los contenidos de las carpetas.
        for image in img_files:
            full_path = path + '/' + image
            image_surf = pygame.image.load(full_path).convert_alpha()
            surface_dict[image.split('.')[0]]  = image_surf
    return surface_dict