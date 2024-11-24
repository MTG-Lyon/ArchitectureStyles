@Hexagonal
#@Clean
#@VerticalSlice

Feature: Publish an event
In order to allow participants to join an event
As a organizer
I want to publish an event

Scenario: Publish status is displayed on the event list
    Given a new event "Software Maintenance Costs" created
    When I publish the event "Software Maintenance Costs"
    Then the event list is
      | Name                       | Status    |
      | Software Maintenance Costs | Published |

@ErrorHandling
Scenario: Cannot publish an already published event
    Given a new event "Software Maintenance Costs" created
    When I publish the event "Software Maintenance Costs"
    And I publish the event "Software Maintenance Costs"
    Then a forbidden error occurred with message "The event is already published"