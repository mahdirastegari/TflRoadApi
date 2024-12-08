Feature: Console Application
As a user, I want to use a console application to retrieve road information or receive an informative error for invalid road IDs,
so I can quickly understand road conditions or identify input issues.

Scenario: Valid road ID
    Given I have entered the road ID "A23"
    Then the application should display:
        """
        The status of the A23 is as follows
            Road Status is Good
            Road Status Description is No Exceptional Delays
        """
    And it should exit with code 0

  Scenario: Invalid road ID
    Given I have entered the road ID "A233"
    Then the application should display:
    """
    A233 is not a valid road
    """
    And it should exit with code 2

  Scenario: No arguments provided
    Given I have entered the road ID ""
    Then the application should display:
    """
    Please provide Road Id.
    """
    And it should exit with code 1

  Scenario: Too many arguments provided
    Given I have entered the road ID "A1 A2"
    Then the application should display:
    """
    Too many arguments.
    """
    And it should exit with code 1
