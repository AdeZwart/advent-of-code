import aoc


def part_one():
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

        aoc.print_answer(7, 1, "")


def part_two():
    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

        aoc.print_answer(7, 2, "")
