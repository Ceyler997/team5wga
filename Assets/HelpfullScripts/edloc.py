import json
import re
from os import listdir


def get_dict(file_path):
    file = open(file_path)
    content = file.read()
    file.close()
    array_from_loc = json.loads(content)["items"]
    localization = {}
    for array_line in array_from_loc:
        localization[array_line["key"]] = array_line["value"]
    return localization


def get_file_names(dir_path):
    try:
        file_list = listdir(dir_path)
        for file_name in file_list:
            if not re.fullmatch('.*Loc.json', file_name):
                file_list.remove(file_name)
        return file_list
    except FileNotFoundError:
        return []


def get_confirmation():
    while True:
        print("Print [yes] or [no]")
        answer = input(">")
        if re.fullmatch("[Yy]|[Yy]es", answer):
            return True
        elif re.fullmatch("[Nn]|[Nn]o", answer):
            return False


def print_help():
    print("Existing commands:")
    print("help - show this message")
    print("addloc [<file name>...] - add new localization files (Loc.json will be added automatically)")
    print("addkey [<key>...] - add new key with value to existing files")
    print("delkey [<key>...] - delete key with value from existing files")
    print("showkey [<key>...] - show key with value from existing files")
    print("redkey [<key>...] - change key value in existing files")
    print("renamekey [<key>...] - change key name in existing files")
    print("showall [<file name>...] - show all keys with values from file (Loc.json will be added automatically)")
    print("showall - show all keys with values from existing files")
    print("save - save changes to files")
    print("exit - exit program. 'save' will be called before exit")
    print("quit - same as 'exit'")


def add_loc(args):
    print("You are going to add localizations:")
    for file_name in args:
        print(file_name + "Loc.json")
    print("Are you sure?")
    if get_confirmation():
        for file_name in args:
            file_name += "Loc.json"
            if file_name in locDict:
                print("There is existing localization file", file_name)
            else:
                locDict[file_name] = {}
                for key in locDict[exsDict]:
                    add_key(file_name, key)
            print()


def add_key(localization, key):
    value = ""
    print("Enter value for", key, "in", localization)
    while len(value) is 0:
        value = input(">")
    locDict[localization][key] = value


def add_keys(args):
    print("You are going to add keys", args)
    print("Are you sure?")
    if get_confirmation():
        for key in args:
            if key not in locDict[exsDict]:
                for localization in locDict:
                    add_key(localization, key)
            else:
                print(key, "is already existing")
                print("Use 'redkey' to change key value")
    print()


def del_key(args):
    print("You are going to delete keys ", args)
    print("Are you sure?")
    if get_confirmation():
        for localization in locDict:
            for key in args:
                if key in locDict[localization]:
                    del locDict[localization][key]
                    print("Key", key, "was removed from", localization)
                else:
                    print("Key", key, "was not founded in", localization)
            print()


def show_key(args):
    for localization in locDict:
        print(localization)
        for key in args:
            if key in locDict[localization]:
                print(key, ":", locDict[localization][key])
            else:
                print(key, "is not founded")
        print()


def redact_key(args):
    print("You are going to change keys", args)
    print("Are you sure?")
    if get_confirmation():
        for key in args:
            if key in locDict[exsDict]:
                for localization in locDict:
                    add_key(localization, key)
            else:
                print("There is no key '" + key + "'")
                print("Use 'addkey' to add new key")
    print()


def rename_key(args):
    print("You are going to rename keys", args)
    print("Are you sure")
    if get_confirmation():
        for key in args:
            if key in locDict[exsDict]:
                print("Enter new name for key", key)
                new_key = input(">")

                while new_key in locDict[exsDict]:
                    print("Key", new_key, "is already exists")
                    print("Please, enter another name")
                    new_key = input(">")

                for localization in locDict:
                    locDict[localization][new_key] = locDict[localization].pop(key)
            else:
                print("There is no key '" + key + "'")
                print("Use 'addkey' to add new key")
            print()


def show_all(args):
    if len(args) is 0:
        for key in locDict[exsDict]:
            print(key)
            for localization in locDict:
                print(localization, ":", locDict[localization][key])
            print()
    else:
        for key in locDict[exsDict]:
            print(key)
            for arg in args:
                arg_file_name = arg + "Loc.json"
                if arg in locDict:
                    print(arg, ":", locDict[arg][key])
                elif arg_file_name in locDict:
                    print(arg_file_name, ":", locDict[arg_file_name][key])
                else:
                    print(arg_file_name, ": no file for this localization")
            print()


def save():
    print("Save changes from last saving?")
    if get_confirmation():
        for file_name in locDict:
            items_arr = []
            for key in locDict[file_name]:
                temp_dict = {"key": key, "value": locDict[file_name][key]}
                items_arr.append(temp_dict)
            loc_file = open(locFolderName + file_name, 'w')
            json.dump({"items": items_arr}, loc_file)
            loc_file.close()


def quit_func():
    save()
    quit()


userRespond = ""
locDict = {}  # len(dict)
locFolderName = "..\\StreamingAssets\\"

locFiles = get_file_names(locFolderName)
if len(locFiles) is 0:
    locFolderName = "StreamingAssets\\"
    locFiles = get_file_names(locFolderName)
if len(locFiles) is 0:
    locFolderName = ""
    locFiles = get_file_names(locFolderName)

if len(locFiles) is not 0:
    for fileName in locFiles:
        locDict[fileName] = get_dict(locFolderName + fileName)
    exsDict = locFiles[0]
    print("Print 'help' for list of command")
else:
    exsDict = 0
    print("Localization files was not founded")
    print("Print 'help' and make sure that you run script from the right place")

while True:
    userRespond = input(">").split()
    if userRespond[0] == 'help' or exsDict is 0:
        print_help()

    elif userRespond[0] == 'addloc':
        userRespond.remove('addloc')
        add_loc(userRespond)

    elif userRespond[0] == 'addkey':
        userRespond.remove('addkey')
        add_keys(userRespond)

    elif userRespond[0] == 'delkey':
        userRespond.remove('delkey')
        del_key(userRespond)

    elif userRespond[0] == 'showkey':
        userRespond.remove('showkey')
        show_key(userRespond)

    elif userRespond[0] == 'redkey':
        userRespond.remove('redkey')
        redact_key(userRespond)

    elif userRespond[0] == 'showall':
        userRespond.remove('showall')
        show_all(userRespond)

    elif userRespond[0] == 'renamekey':
        userRespond.remove('renamekey')
        rename_key(userRespond)

    elif userRespond[0] == 'save':
        save()

    elif userRespond[0] == 'quit' or userRespond[0] == 'exit':
        quit_func()
        break
    else:
        print("Unknown command")
        print_help()
