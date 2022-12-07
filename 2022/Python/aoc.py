import os


def get_file_path(dirname):
    return os.path.join(os.path.dirname(dirname), "input.txt")


def print_answer(day_number, part_number, answer):
    print(f"The answer of day {day_number} part {part_number} is: {answer}")
