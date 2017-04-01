# SimpleObjectAppender

Simple log4net extensions - extract any data that you want from your object.

**The idea of the ObjectAppender was originally brought by [@eliranmoyal](https://github.com/eliranmoyal) and [@DimaRabkin](https://github.com/DimaRabkin).**

This specific project started as a simple and abstract example of how can this be achieved and I decided to upload it.

The SimpleObjectAppender gives you the ability to:

```c#
// Say you have this class:
class Person
{
    public int Age { get; set; }
    public string Name { get; set; }
}
```

```c#
// So INSTEAD of logging like this:
logger.Info(string.Format("Person details - Name:{0}, Age:{1}", person.Name, person.Age))

// You would do this:
logger.Info(person);
```

And choose which properties of the class you want to log and in which format *inside* your log4net configuration.

---

Project contains:
 * [SimpleObjectForwarder][1] - Forwarding Appender.
 * [SimpleObjectConverter][2] - Layout Converter.
 * [SimpleObjectConsoleAppender][3] - Console Appender.

---

## SimpleObjectForwarder

A log4net [forwarding appender][4] ([example][21]) which will format your object as string and send it to next appenders.

### Usage example:

```xml
 <appender name="objectForwarder" type="SimpleObjectAppender.SimpleObjectForwarder, SimpleObjectAppender">
    <appender-ref ref="nextAppenderName" />
    <Details>
      <Property>Prop1Name</Property>
      <Property>Prop2Name</Property>
    </Details>
  </appender>
```

More Tags:
 * Separator - The separator string between each property. *(default: ", ")*
 * EqualsSymbol - The string which separate between property name and value. *(default: ":")*
 * IgnoreNotExists - Ignore the whole key+value if the property doesn't exists. *(default: true)*
 * Descriptor - If you are pro and want custom property extractor.

## SimpleObjectConverter

A log4net [pattern layout converter][5] ([example][22] of creating and using converter) which will give you the ability to format the message with your own object properties.

### Usage example:

```xml
 <appender name="MyConsoleAppender" type="log4net.Appender.ConsoleAppender">
    <layout type="log4net.Layout.PatternLayout">
      
      <Converter>
        <name value="object" />
        <type value="SimpleObjectAppender.SimpleObjectConverter, SimpleObjectAppender" />
      </Converter>
      
      <conversionPattern value="%-4timestamp %-5level %logger - Name is:%object{Name}, age:%object{Age}%newline" />
    </layout>
  </appender>
```

## SimpleObjectConsoleAppender

A [log4net console appender][6] ([example][23]) - just a console appender, the configuration is exactly as the forwarding appender except that this appender will just write to you console instead of forwarding the message.

### Usage example:

```xml
 <appender name="objectConsoleAppender" type="SimpleObjectAppender.SimpleObjectConsoleAppender, SimpleObjectAppender">
    <Details>
      <Property>prop1</Property>
      <Property>prop2</Property>
    </Details>
  </appender>
```

[1]: https://github.com/urielha/SimpleObjectAppender#SimpleObjectForwarder
[2]: https://github.com/urielha/SimpleObjectAppender#SimpleObjectConverter
[3]: https://github.com/urielha/SimpleObjectAppender#SimpleObjectConsoleAppender
[4]: https://logging.apache.org/log4net/release/sdk/?topic=html/T_log4net_Appender_ForwardingAppender.htm
[5]: https://logging.apache.org/log4net/release/sdk/?topic=html/T_log4net_Layout_Pattern_PatternLayoutConverter.htm
[6]: https://logging.apache.org/log4net/release/sdk/?topic=html/T_log4net_Appender_AppenderSkeleton.htm

[21]: https://logging.apache.org/log4net/release/config-examples.html#ForwardingAppender
[22]: https://devstuffs.wordpress.com/2012/01/12/creating-your-own-pattern-layout-converter-for-log4net/
[23]: https://logging.apache.org/log4net/release/config-examples.html#ConsoleAppender
