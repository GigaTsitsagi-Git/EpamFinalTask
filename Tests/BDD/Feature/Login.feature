Feature: Login functionality

  Scenario Outline: User logs in successfully on <browser>
    Given I run on <browser>
    And I open the login page
    When I enter valid username and password
    And I click the login button
    Then I should see the home page

    Examples:
      | browser |
      | Edge    |
      | Firefox |