@Hexagonal
#@Clean
#@VerticalSlice

Feature: Join an event
In order to indicate I want to join an event
As a participant
I want to join an existing event

Background:
    Given a new event "Software Maintenance Costs" created

@ErrorHandling
Scenario: Cannot join an draft event
    When I join the event "Software Maintenance Costs" as "john.doe@example.com"
    Then a forbidden error occurred with message "The event is not published yet"

Scenario: Once joined, participants can be listed
    Given the event "Software Maintenance Costs" is published
    When I join the event "Software Maintenance Costs" as "john.doe@example.com"
    Then the "Software Maintenance Costs" event participant list is
      | Email address        |
      | john.doe@example.com |

@ErrorHandling
Scenario: Cannot join an event twice
    Given the event "Software Maintenance Costs" is published
    When I join the event "Software Maintenance Costs" as "john.doe@example.com"
    And I join the event "Software Maintenance Costs" as "john.doe@example.com"
    Then a forbidden error occurred with message "The participant has already joined"

@ErrorHandling
Scenario: By default, events have a max participant limit to 10
    Given the event "Software Maintenance Costs" is published
    And 10 participants have joined the event "Software Maintenance Costs"
    When I join the event "Software Maintenance Costs" as "john.doe@example.com"
    Then a forbidden error occurred with message "The event has reached its maximum participant limit"