import json
import math
from collections import Counter
from datetime import datetime

# Read JSON file
with open("Assets/eventLogs.json", "r") as file:
    data = json.load(file)

events = data["events"]

# Count fires at each sensor
fire_counter = Counter()

for event in events:
    message = event["eventMessage"]

    if "Fire detected at" in message:
        sensor = message.split("Fire detected at ")[1]
        fire_counter[sensor] += 1

# Total fires
total_fires = sum(fire_counter.values())

print("\n===== WILDFIRE REPORT =====\n")

print("Total Fires:", total_fires)
print()

print("Fires Per Sensor:")

for sensor, count in fire_counter.items():
    print(sensor, ":", count)

print()

if len(fire_counter) > 0:
    most_dangerous_sensor = max(
        fire_counter,
        key=fire_counter.get
    )

    print(
        "Most Dangerous Sensor:",
        most_dangerous_sensor
    )
    
    print()

response_times = []
extinguish_times = []
mission_times = []

fire_detected_time = None
drone_reached_time = None

for event in events:

    current_time = datetime.strptime(
        event["time"],
        "%H:%M:%S"
    )

    message = event["eventMessage"]

    if "Fire detected at" in message:
        fire_detected_time = current_time

    elif "Drone reached fire" in message:
        if fire_detected_time is not None:
            response_time = (
                current_time -
                fire_detected_time
            ).total_seconds()

            response_times.append(
                response_time
            )

            drone_reached_time = current_time

    elif "Fire extinguished" in message:
        if (
            fire_detected_time is not None and
            drone_reached_time is not None
        ):

            extinguish_time = (
                current_time -
                drone_reached_time
            ).total_seconds()

            mission_time = (
                current_time -
                fire_detected_time
            ).total_seconds()

            extinguish_times.append(
                extinguish_time
            )

            mission_times.append(
                mission_time
            )

            fire_detected_time = None
            drone_reached_time = None

    print()

if len(response_times) > 0:

    avg_response = (
        sum(response_times) /
        len(response_times)
    )

    avg_extinguish = (
        sum(extinguish_times) /
        len(extinguish_times)
    )

    avg_mission = (
        sum(mission_times) /
        len(mission_times)
    )

    print(
        "Average Response Time:",
        round(avg_response,2),
        "sec"
    )

    print(
        "Average Extinguishing Time:",
        round(avg_extinguish,2),
        "sec"
    )

    print(
        "Average Mission Time:",
        round(avg_mission,2),
        "sec"
    )
    print()
print("===== SENSOR RANKING =====")
print()

rank = 1

sorted_sensors = sorted(
    fire_counter.items(),
    key=lambda x: x[1],
    reverse=True
)

for sensor, count in sorted_sensors:

    percentage = (
        count * 100 /
        total_fires
    )

    print(
        f"{rank}. {sensor} → {count} fires ({percentage:.2f}%)"
    )

    rank += 1
    print()
print("===== DISTANCE ANALYSIS =====")
print()

sensor_positions = {
    "Sensor_01": (200, 5, 200),
    "Sensor_02": (200, 5, 300),
    "Sensor_03": (300, 7, 300),
    "Sensor_04": (300, 5, 200),
    "Sensor_05": (250, 2, 250)
}

fire_sequence = []

for event in events:

    message = event["eventMessage"]

    if "Fire detected at" in message:

        sensor = message.split(
            "Fire detected at "
        )[1]

        fire_sequence.append(sensor)

total_distance = 0
longest_mission = 0

for i in range(1, len(fire_sequence)):

    prev_sensor = fire_sequence[i - 1]
    curr_sensor = fire_sequence[i]

    x1, y1, z1 = sensor_positions[prev_sensor]
    x2, y2, z2 = sensor_positions[curr_sensor]

    distance = math.sqrt(
        (x2 - x1) ** 2 +
        (y2 - y1) ** 2 +
        (z2 - z1) ** 2
    )

    total_distance += distance

    longest_mission = max(
        longest_mission,
        distance
    )

if len(fire_sequence) > 1:

    average_distance = (
        total_distance /
        (len(fire_sequence) - 1)
    )

    print(
        "Total Distance Travelled:",
        round(total_distance,2)
    )

    print(
        "Average Mission Distance:",
        round(average_distance,2)
    )

    print(
        "Longest Mission:",
        round(longest_mission,2)
    )

    print()
print("===== ZONE ANALYSIS =====")
print()

zone_map = {
    "Sensor_01": "Southwest",
    "Sensor_02": "Northwest",
    "Sensor_03": "Northeast",
    "Sensor_04": "Southeast",
    "Sensor_05": "Center"
}

zone_counter = Counter()

for sensor, count in fire_counter.items():

    zone = zone_map[sensor]

    zone_counter[zone] += count

for zone, count in zone_counter.items():

    print(
        zone,
        ":",
        count,
        "fires"
    )

print()

most_dangerous_zone = max(
    zone_counter,
    key=zone_counter.get
)

print(
    "Most Dangerous Zone:",
    most_dangerous_zone
)