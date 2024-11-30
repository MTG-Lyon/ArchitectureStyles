Feature: Create a new event
In order to gather participants around a specific topic
As a organizer
I want to create a new event

@ErrorHandling
Scenario: Cannot create an event without name
    When I create a new event "     "
    Then a bad request error occurred

@ErrorHandling
Scenario: Cannot create an event with a name that is already taken
    When I create a new event "Software Maintenance Costs"
    And I create a new event "Software Maintenance Costs"
    Then a forbidden error occurred with message "The event name is already taken"

Scenario: Created event is listed with draft status by default
    When I create a new event "Software Maintenance Costs"
    Then the event list is
      | Name                       | Status |
      | Software Maintenance Costs | Draft  |