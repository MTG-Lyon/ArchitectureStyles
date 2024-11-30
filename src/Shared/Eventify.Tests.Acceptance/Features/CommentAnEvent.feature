Feature: Comment an event
In order to ask question or answer to questions concerning the event
As a participant
I want to comment the event

Background:
    Given the current date is 2024-11-01
    And a new event "Software Maintenance Costs" created
    And the event "Software Maintenance Costs" is published
    And the event "Software Maintenance Costs" has been joined by "john.doe@example.com"
    And the event "Software Maintenance Costs" has been joined by "johanna.doe@example.com"
    
@ErrorHandling
Scenario: User cannot comment an event if they did not join the event
    When "unknown@example.com" comments on the event "Software Maintenance Costs" with "At what time is the event?"
    Then an forbidden error occurred with message "A user who has not joined the event cannot comment on it"
    
Scenario: Comments are listed in the event details in chronological order
    When "john.doe@example.com" comments on the event "Software Maintenance Costs" with "At what time is the event?"
    And 1 day passes
    And "johanna.doe@example.com" comments on the event "Software Maintenance Costs" with "I think it is at 10:00 AM."
    Then the event "Software Maintenance Costs" details contains the following comments
      | Date       | Commenter               | Comment                    |
      | 2024-11-01 | john.doe@example.com    | At what time is the event? |
      | 2024-11-02 | johanna.doe@example.com | I think it is at 10:00 AM.  |

Scenario: Participants who commented on the event receive an email notification when a new comment is added
    Given "john.doe@example.com" has commented on the event "Software Maintenance Costs" with "At what time is the event?"
    When "johanna.doe@example.com" comments on the event "Software Maintenance Costs" with "At what time is the event?"
    Then the following emails have been sent
        | Email address        | Subject                | Content                                                                         |
        | john.doe@example.com | Eventify - New comment | johanna.doe@example.com has commented on the event "Software Maintenance Costs" |