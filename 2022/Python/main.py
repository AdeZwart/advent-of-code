import sys
import time
from day1.main import part_one as day1_part1, part_two as day1_part2
from day2.main import part_one as day2_part1, part_two as day2_part2
from day3.main import part_one as day3_part1, part_two as day3_part2
from day4.main import part_one as day4_part1, part_two as day4_part2
from day5.main import part_one as day5_part1, part_two as day5_part2
from day6.main import part_one as day6_part1, part_two as day6_part2
from day7.main import part_one as day7_part1, part_two as day7_part2
from day8.main import part_one as day8_part1, part_two as day8_part2
from day9.main import part_one as day9_part1, part_two as day9_part2


def main():
    if (len(sys.argv) > 1):
        day_to_run = int(sys.argv[1])
    else:
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
        case 7:
            day7_part1()
            day7_part2()
        case 8:
            day8_part1()
            day8_part2()
        case 0:
            start_time = time.time()
            day1_part1()
            day1_part2()
            day2_part1()
            day2_part2()
            day3_part1()
            day3_part2()
            day4_part1()
            day4_part2()
            day5_part1()
            day5_part2()
            day6_part1()
            day6_part2()
            day7_part1()
            day7_part2()
            day8_part1()
            day8_part2()
            day9_part1()
            day9_part2()
            print(f"--- {time.time() - start_time} seconds ---")
        case _:
            print(f"Day {day_to_run} is not (yet) implemented!")


if __name__ == '__main__':
    main()
