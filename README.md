# Coursework

| <sub><b>Lenkelheit</b></sub>| <sub><b>iamprovidence</b></sub>|
| :---: | :---: |
| [<img src="https://avatars3.githubusercontent.com/u/38116562?s=400&v=4" width="100px;"/>](https://github.com/Lenkelheit) | [<img src="https://avatars3.githubusercontent.com/u/24938726?s=400&v=4" width="100px;"/>](https://github.com/iamprovidence) |
|Nazariy Tymtsiv|Taras Kizlo|


# Contents
* [Convention](#convention)
  - [Repository Convention](#repository-convention)
    - [Project Configuration](#project-configuration)
    - [Politics](#politics)
    - [Project Convention](#project-convention)
    - [Log Convention](#log-convention)
    - [Test Convention](#test-convention)
    - [Github Convention](#github-convention)
    - [File Hierarchy](#file-hierarchy)
    - [Team Work](#team-work)
  - [Coding Convention](#coding-convention)
    - [General](#general)
    - [Naming](#naming)
    - [Formating](#formating)

## Convention

### Repository Convention

#### Project Configuration

C# version: 4.0

.NET version: 4.5.2

#### Politics

* all decisions are made through voting
* if somebody missed voting it is his own fault
* after voting has passed whining is not allowed

#### Project Convention

* everybody create a new brunch for his work
* one respons for one brunch
* if somebody want to do changes in someone's bruch he should create separate brunch
* all issues should have labels, assignees and should be bond to project
* all pull requests should have labels, assigness and should be bond to project

#### Log Convention

Log Messages has next importancy levels:
- ![#e5e5e5](https://placehold.it/15/e5e5e5/000000?text=+) `debug` — explain logic step by step
- ![#ffff46](https://placehold.it/15/ffff46/000000?text=+) `info` — information about work (efficiency)
- ![#ffc346](https://placehold.it/15/ffc346/000000?text=+) `warn` — something weird happen
- ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) `error` — error has been occurred
- ![#000000](https://placehold.it/15/000000/000000?text=+) `fatal` — immediately help required


#### Test Convention

* one-unit test = one testing block
* if block testing depend on other objects, it should be mocked
* unit test can contain multiple scenarios, but they should be separated
* each scenario should match next template

```C#
[TestMethod]
public void TestingBlockName()
{
  // BEHAVIOR 
  // the beginning balance should be reduced by debit amount
  // TAKE
  // passed regular parameter
  // debit amount is less than beginnig balance
  // RETURN
  // a positive value after money has been dabit
  
  // Arrange
  double beginningBalance = 11.99;
  double debitAmount = 4.55;
  double expected = 7.44;
  BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

  // Act
  account.Debit(debitAmount);

  // Assert
  double actual = account.Balance;
  Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
}
```

#### Github Convention

Issues Limit - **15**

Branches name:

* Repository's branches
  - master — branch for clients
  - development — branch for developers

* Your branches
  - feature/FeatureName
  - fix/FixName

#### File Hierarchy

Unit-test and main project in one solution but in different projects.

Each project has the **"Resources"** , **"Models"** folder with inners folders
- Classes
- Interfaces
- EventArgClasses
- ExtensionClasses
- etc

And **"Components"** folder:
- Forms
- UserControls
- etc

Additional folders depend on projects.

#### Team Work

After a new task become available we should divide it on several tasks.

Then with a voting process everybody get his own tasks, pointing out a deadline for his work. 

The team group up to discuss the architecture of the program (classes, file hierarchy, etc).

The one should do unit test and documentation to his part of work.

If one do not has complete work for his own deadline time, the task pass to a free teammate.

The one can set his deadline 10 days before task deadline.

All unit-tests should be reworked in 2 days after project complete.

After work has been done we sum up what was done right and what was not.

### Coding Convention

#### General

- Configurable Data
  - do keep configurable data (using, const, static etc) at high levels
- Cycles
  - do use ++i not i++, iterator objects have different speed cost
- String
  - use StringBuilder for string manipulation
  - use all kind of string formating (String.Format(), @, $, {}, 0#.##, etc)
- Variable 
  - do not use *magic numbers*(*magic strings*), better use constants
- Fields
  - do not use public fields
  - distinguish constant and readonly
  - do not initialize fields in declaration, do use constructors
- Properties
  - do use read-only properties
  - do not use write-one properties, better use method
  - do use annonymous properties only in standalone, not important classes
  - if properties return field, it should has the same name
- Methods
  - one method = one action
  - method's body shoud be in 25-50 lines range
  - private methods for inner complex instructions
  - event methods should start with On- preffix
  - if variable names, that you pass, are not clear enough, use named parameters
- Events
  - **Two arguments**:
    - sender, object — object that has generated event
    - e — instance of EventArgs, contain event data
- Exception Handling
  - do not ignore cathed exception
    - some special exception can be ignored : ThreadAbortException
  - do hide errors from user, if can not handle exception show user a message
  - log exception in files with all details (type, time, method's name, class' name etc)
  - do not catch all exception, only specific ones
- Namespaces
  - Prefer full names to using command if you use namespace's objects infrequently
- Classes
  - do use partial classes for long classes files (over 10.000 lines)
    - fields, constructors in one part
    - methods, properties, indexers, events in another part
  - do not initialize class' properties after initialized instance, do use initialization by name
- Constructors
  - do initialize all fields explicitly
- Collections
  - do use only generic collection over object-based ones
- Files
  - each class in seperate file
  - file's name same as class' name
- Comments
  - do separate different code section with uppercase comments in important classes: FIELDS, METHODS, CONSTRUCTORS(constructor, destructor, methods that create object) ...
  - every *public* and *protected* blocks of code should have XML-documentation
  - every *private* block of code should has comments
  - avoid block of comments 
  - full sentence for comments
  - comments should be up in date
  - comments should be clear and understandable
  - single-line comments preffered over end-line ones
  - do use comments in complex block of codes

#### Naming

- "What?" not "How?"
- Make a sense
- Avoid negative conditional
```diff
- HasNotValue
+ HasValue
```
- Avoid redundency
```diff
- List.ListItem
+ List.Item
```
- Variable 
  - ‘i’, ‘j’, ’k’, ’l’, ’m’, ’n’  - cycles 
  - ‘x’, ‘y’, ‘z‘ -  coordinate
  - ‘r’, ‘g’, ‘b’  - colors
  - ‘e’  - events
  - ‘ex’  - exceptions
- GUI
  - use short suffix
    - Button —> btn
    - TextBox,RichTextBox —> tb
    - ComboBox, CheckBox —> cb
    - DataGridView —> dgv
  - if you don't use control, do not give him name, for inctance Splitter, Label
  - do use simple names for dynamic controls 
  

|  Object Name     |     Type     |  Notation  | Length | Plural | Prefix  | Suffix | Abbreviation | Char Mask   | Underscores |
|:-----------------|:------------:|:----------:|:------:|:------:|:-------:|:-------|:------------:|:------------|:-----------:|
| Assemblies       |Nouns(Company.Component)   | PascalCase | 50 | Y/N | No| No     | No           | [A-z]       | No          |
| Namespace        |Noun          | PascalCase |    50  | Y/N    | No      | No     | No           | [A-z]       | No          |
| Interface        |Noun or Nouns | PascalCase |    128 | No     | Yes     | No     | No           | {**I**}[A-z]| No          |
| Struct           |Noun or Nouns | PascalCase |    128 | No     | No      | Yes    | No           | [A-z][0-9]  | No          |
| Class            |Noun or Nouns | PascalCase |    128 | No     | No      | Yes    | No           | [A-z][0-9]  | No          |
| Constructor      |Same as class | PascalCase |    128 | No     | No      | Yes    | No           | [A-z][0-9]  | No          |
| Event Class      |Nouns         | PascalCase |    128 | No     | No      | Yes    | No           | [A-z][0-9]{EventArgs}| No |
| Attribute class  |Nouns         | PascalCase |    128 | No     | No      | Yes    | No           | [A-z][0-9]{Attribute}| No |
| Method           |Verbs         | PascalCase |    128 | Yes    | No      | No     | No           | [A-z][0-9]  | No          |
| Method arguments |Depend on type| camelCase  |    128 | Yes    | No      | No     | Yes          | [A-z][0-9]  | No          |
| Generic argument |Noun          | PascalCase |    50  | Yes    | No      | No     | Yes  |{**T**}[A-z][0-9]{Key,EventArgs}|No|
| Local variables  |Noun or Nouns | camelCase  |    50  | Yes    | No      | No     | Yes          | [A-z][0-9]  | No          |
| Constants name   |Noun or Nouns | UPPERCASE  |    50  | No     | No      | No     | No           | [A-z][0-9]  | Yes         |
| Field            |Noun or Nouns | camelCase  |    50  | Yes    | No      | No     | Yes          | [A-z][0-9]  | Yes         |
| Boolean Fields   |Noun or Nouns | camelCase  |    50  | Yes    | Yes     | No     | Yes    | {is,can,has,does+}[A-z][0-9]|Yes|
| Properties       |Same as field | PascalCase |    50  | Yes    | No      | No     | Yes          | [A-z][0-9]  | No          |
| Delegate         |Nouns         | PascalCase |    128 | No     | No      | Yes    | Yes          | [A-z]{EventHandler}| No   |
| Events           |Nouns         | PascalCase | 128    | No     | No      | Yes    | Yes          | [A-z]{Changed}| No        |
| Enum type        |Noun(regular) or Nouns(bit fields)|PascalCase|128|Yes|No|No     | No           | [A-z]         | No        |
| GUI              |Noun or Nouns |HungarianNotation|50 | Yes    | Yes     | Yes    | Yes          | [A-z]         | No        |
| GUI events       |ObjName + _ + EventName |PascalCase |128| No | No      | Yes    | Yes          | [A-z]{Changed}| Yes       |

#### Formating

* Padding
  - every block of code should has padding depending on outer block
  - padding should be done with tabulation
  - do use empty lines to divide logic
  - constanst and enums should be align on their types, names, operators etc
  ```C#
  public const int DBVERSION        = 4;
  public const int TINYINT_OWERFLOW = 8115;
  public const int TRIGGER_EXCEPT   = 50000;

  public enum StatusMode
  {
     		Planned  = 1,
     		Active   = 2,
     		InActive = 4,
     		All      = 7
  };

  ```
  - whitespaces after and before operators
  ```diff
  -isDisposing=false;
  +isDisposing = false;
  ```
  - long boolean statements should be divided by &&, || operators or incapsulated in variable or method
* curve brackets
  - every block of code (cycles, condition statements etc) should has curve brackets (exception underneath)
  - vertical brackets allignment
  ```diff
  - if (...){
  -}
  
  + if (...)
  +{
  +}
  ```
* code length
  - code line length should be less than 80 symbols
  - use shortage notation for short block of code
  - avoid curve brackets in short block of code
* one line = one command 

