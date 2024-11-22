Feature: Create a new event
In order to gather participants around a specific topic
As a organizer
I want to create a new event

@ErrorHandling
Scenario: Cannot create an event without name
    When I create a new event ""
    Then an error occurred with message "The event name is required"

Scenario: Created event are listed
    When I create a new event "Software Maintenance Costs"
    Then the event list is
        | Name |
        | Software Maintenance Costs |