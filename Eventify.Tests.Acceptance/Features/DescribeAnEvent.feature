@Hexagonal
#@Clean
#@VerticalSlice

Feature: Describe an event
In order to make the event attractive
As a organizer
I want to describe the event

@ErrorHandling
Scenario: Cannot describe an unknown event
    When I describe an unknown event
    Then a not found error occurred

Scenario: New description is listed in the event list
    Given a new event "Software Maintenance Costs" created
    When I describe the event "Software Maintenance Costs" with "This event will be organized by M. Gourou"
    Then the event list is
      | Name                       | Description                               |
      | Software Maintenance Costs | This event will be organized by M. Gourou |