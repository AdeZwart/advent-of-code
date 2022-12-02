import os
FILE_NAME = "input.txt"


def get_file_path():
    return os.path.join(os.path.dirname(__file__), FILE_NAME)


def print_answer(part_number, answer):
    print(f"The answer of part {part_number} is: {answer}")


def part_one():
    elf_count = 0
    elves = [0]

    with open(get_file_path()) as f:
        for line in f:
            calories = line.strip()

            if (calories == ""):
                elf_count += 1
                elves.append(0)
            else:
                elves[elf_count] += int(calories)

    elves.sort()
    elves.reverse()
    print_answer(1, elves[0])


def part_two():
    elf_count = 0
    elves = [0]

    with open(get_file_path()) as f:
        for line in f:
            calories = line.strip()

            if (calories == ""):
                elf_count += 1
                elves.append(0)
            else:
                elves[elf_count] += int(calories)

    elves.sort()
    elves.reverse()

    total = elves[0] + elves[1] + elves[2]
    print_answer(2, total)


if __name__ == '__main__':
    print("-- Run part 1 --")
    part_one()

    print("-- Run part 2 --")
    part_two()
