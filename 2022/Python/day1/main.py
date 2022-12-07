import aoc


def part_one():
    elf_count = 0
    elves = [0]

    with open(aoc.get_file_path(__file__)) as f:
        for line in f:
            calories = line.strip()

            if (calories == ""):
                elf_count += 1
                elves.append(0)
            else:
                elves[elf_count] += int(calories)

    elves.sort()
    elves.reverse()
    aoc.print_answer(1, 1, elves[0])


def part_two():
    elf_count = 0
    elves = [0]

    with open(aoc.get_file_path(__file__)) as f:
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
    aoc.print_answer(1, 2, total)
