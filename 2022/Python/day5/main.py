import aoc
import re


def part_one():
    cargo = get_cargo()

    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

            procedure = re.findall('\d+', line)

            cargo = rearrange_cargo_9000(cargo, procedure)

        message = get_message(cargo)

    aoc.print_answer(1, message)


def part_two():
    cargo = get_cargo()

    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            line = line.rstrip()

            procedure = re.findall('\d+', line)

            cargo = rearrange_cargo_9001(cargo, procedure)

        message = get_message(cargo)

    aoc.print_answer(2, message)


def get_cargo():
    # cargo = [["Z", "N"], ["M", "C", "D"], ["P"]]
    cargo = [
        ["H", "B", "V", "W", "N", "M", "L", "P"],
        ["M", "Q", "H"],
        ["N", "D", "B", "G", "F", "Q", "M", "L"],
        ["Z", "T", "F", "Q", "M", "W", "G"],
        ["M", "T", "H", "P"],
        ["C", "B", "M", "J", "D", "H", "G", "T"],
        ["M", "N", "B", "F", "V", "R"],
        ["P", "L", "H", "M", "R", "G", "S"],
        ["P", "D", "B", "C", "N"]
    ]

    return cargo


def get_message(cargo):
    message = ""

    for c in cargo:
        if len(c) > 0:
            message += c[-1]

    return message


def rearrange_cargo_9000(cargo, procedure):
    crate_count = int(procedure[0])
    crate_source = int(procedure[1])-1
    crate_target = int(procedure[2])-1

    for _ in range(0, crate_count):
        cargo[crate_target].append(cargo[crate_source][-1])
        cargo[crate_source].pop()

    return cargo


def rearrange_cargo_9001(cargo, procedure):
    crate_count = int(procedure[0])
    crate_source = int(procedure[1])-1
    crate_target = int(procedure[2])-1

    crates_to_move = cargo[crate_source][-crate_count:]

    cargo[crate_target] = cargo[crate_target] + crates_to_move
    cargo[crate_source] = cargo[crate_source][0:-crate_count]

    return cargo
