# PPSSPP Texture Dump Tool

## What?

This tool is designed to improve the modder experience when upscaling or modding textures in PPSSPP. It automates part of the renaming and reorganizing process for texture files dumped from PPSSPP, and provides correctly formatted output for the textures.ini file.

## How?

0. Make sure you have dumped the desired textures from PPSSPP. You can locate the dumped textures as "TEXTURE_HASH.png" images in the following directory: `PSP/TEXTURE/GAME_ID/new/`.
1. Open the folder where the dumped textures are located (`PSP/TEXTURE/GAME_ID/new/`) and place the PPSSPPTextureDumpTool.exe inside.
2. Manually rename the texture files and move them to an appropriate folder structure. The supported naming conventions are as follows:


   ## For single textures:
   - Rename from `new/TEXTURE_HASH.png` to `new/path/to/TEXTURE_HASH new_name.png`.
     
   OR
   - Rename from `new/TEXTURE_HASH.png` to `new/path/to/TEXTURE_HASHnew_name.png`.

  
   #### These examples result in the program outputting the following to `textures.txt` for use in `../textures.ini`:
   
   `TEXTURE_HASH = path/to/new_name.png`
   
   #### It changes the filenames to:
   
   `new/path/to/new_name.png`

   
   ## For flipbook animations:
   - Rename from `new/TEXTURE_HASH.png` to `new/path/to_anim/TEXTURE_HASH.png`.
   ### Note: Flipbook animations need to be inside a folder with the `_anim` suffix and hashes as names, no custom names supported for them.
   
   #### These examples result in the program outputting the following to `textures.txt` for use in `../textures.ini`:
   
   `TEXTURE_HASH = path/to_anim/000.png`,
   
   `TEXTURE_HASH = path/to_anim/001.png`,
   
   etc...
   
   #### It changes the filenames to:
   
   `new/path/to/***.png`
   where the files inside the `*_anim` folder are numbered as they're processed (in alphabetical order)
    
3. Once you have renamed and organized all your textures, run the PPSSPP Texture Dump Tool program.
4. The program will recursively scan the directory and generate a `textures.txt` file at the root level.
   - The `textures.txt` file will contain one line per `.png` file found during the scan, following the format:
     ```
     TEXTURE_HASH = path/to/file/new_name_without_hash_and_first_space.png
     ```
5. After the line has been printed to the `textures.txt` file, the program will remove the hash and first space from the corresponding file's name. The file will be renamed from `path/to/HASH file.png` to `path/to/file.png`.

## **Note:** It's important to follow the instructions closely to ensure proper functionality of the PPSSPP Texture Dump Tool.
