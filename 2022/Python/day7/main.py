import aoc
import json


def part_one():
    directories = []
    dir_pointer = ''

    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

            if (line.startswith('$ cd')):
                directories, dir_pointer = change_directory(
                    line[5:], directories, dir_pointer)
            elif (line.startswith('$ ls')):
                # Only shows dir content
                continue

            elif (line.startswith('dir')):
                dir_name = f"{dir_pointer}{line[4:]}" if dir_pointer == '/' else f"{dir_pointer}/{line[4:]}"
                # If we don't have a directory with this name on the collection
                if (find_directory_by_name(directories, dir_name) == None):
                    # Add the dir to the collection
                    directories.append(
                        Directory(dir_name, 0, dir_pointer))
            else:
                file_details = line.split()
                # Get the current directory from the collection
                increment_dir_size(directories, dir_pointer,
                                   int(file_details[0]))

        total_dir_size = 0
        for d in directories:
            if (d.size <= 100000):
                total_dir_size += d.size

        aoc.print_answer(7, 1, total_dir_size)


def part_two():
    directories = []
    dir_pointer = ''

    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

            if (line.startswith('$ cd')):
                directories, dir_pointer = change_directory(
                    line[5:], directories, dir_pointer)
            elif (line.startswith('$ ls')):
                # Only shows dir content
                continue

            elif (line.startswith('dir')):
                dir_name = f"{dir_pointer}{line[4:]}" if dir_pointer == '/' else f"{dir_pointer}/{line[4:]}"
                # If we don't have a directory with this name on the collection
                if (find_directory_by_name(directories, dir_name) == None):
                    # Add the dir to the collection
                    directories.append(
                        Directory(dir_name, 0, dir_pointer))
            else:
                file_details = line.split()
                # Get the current directory from the collection
                increment_dir_size(directories, dir_pointer,
                                   int(file_details[0]))

        root = find_directory_by_name(directories, '/')

        total_disk_space = 70000000
        required_free_space = 30000000

        for d in sorted(directories, key=lambda x: x.size):
            if (d.size >= (required_free_space - total_disk_space - root.size)):
                aoc.print_answer(7, 2, d.size)
                break


def change_directory(cmd: str, directories: list, pointer: str):
    if (cmd == '/'):
        directories.append(Directory(cmd))
        pointer = cmd
    elif (cmd == '..'):

        # get the current directory
        current_dir = find_directory_by_name(
            directories, pointer)
        # set the pointer to its parent dir
        pointer = current_dir.parent
    else:
        # cd {cmd}, set the pointer to the given {cmd}
        pointer = f"{pointer}{cmd}" if pointer == '/' else f"{pointer}/{cmd}"

    return directories, pointer


def increment_dir_size(directories: list, dir_name: str, size: int):
    directory = find_directory_by_name(directories, dir_name)
    if (directory != None):
        directory.add_to_size(size)

        if (directory.parent != None):
            increment_dir_size(directories, directory.parent, size)


def find_directory_by_name(directories: list, name: str):
    for obj in directories:
        json.dumps(obj.__dict__)
        if obj.name == name:
            return obj

    return None


class Directory:
    name = ""
    size = 0
    parent = ""

    def __init__(self, name, size=0, parent=None):
        self.name = name
        self.size = size
        self.parent = parent

    def add_to_size(self, size: int):
        self.size += size
