from day1.main import part_one as day1_part1, part_two as day1_part2
from day2.main import part_one as day2_part1, part_two as day2_part2
from day3.main import part_one as day3_part1, part_two as day3_part2
from day4.main import part_one as day4_part1, part_two as day4_part2
from day5.main import part_one as day5_part1, part_two as day5_part2
from day6.main import part_one as day6_part1, part_two as day6_part2


def main():
    print("Enter a number between 1 and 25 to solve the puzzle for that day:")
    day_to_run = int(input())

    match day_to_run:
        case 1:
            day1_part1()
            day1_part2()
        case 2:
            day2_part1()
            day2_part2()
        case 3:
            day3_part1()
            day3_part2()
        case 4:
            day4_part1()
            day4_part2()
        case 5:
            day5_part1()
            day5_part2()
        case 6:
            day6_part1()
            day6_part2()
        case _:
            print(f"Day {day_to_run} is not (yet) implemented!")


if __name__ == '__main__':
    main()
