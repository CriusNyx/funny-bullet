### Navigation

[Back to README](../README.md)
- [Docs](../Docs.md)
  - [Level Editor](../level/LevelEditor.md)


# Levels

Levels are made out of various components. The most critical is the [Level](#level) component that runs it's children in sequence.

# Components

## Level

The level component runs each of it's children in sequence

## Wave

The wave component looks for children that are sequences and runs them all at the same time.

The wave component will finish and allow the next component to run.

## Spawner

Spawn the prefab with the specified parameters.