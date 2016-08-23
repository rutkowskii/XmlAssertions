using Machine.Specifications;
using System;
using FluentAssertions;
using XmlAssertions;
// ReSharper disable UnusedMember.Local
// ReSharper disable All

namespace XmlAssertionsSamples
{
    public class Samples
    {
        [Subject("Xml asserts")]
        class when_comparing_differing_xml_trees_having_xml_node_name_unmatched
        {
            static Exception exception;

            static string ExceptionMessageExpected =
                @"Node //people[0]/person[1]/documents[2]/paszport[1]; Expected xml node with name [document], but found [paszport]";

            Establish that = () => { };

            private Because of = () => exception = Catch.Exception(() =>
            {
                var actual = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<people>
	<person name=""Piotr"" surname=""Rutkowski"">
	</person>
	<person name=""Jan"" surname=""Kowalski"">
		<address street=""Mickiewicza"" number=""20"" />
		<address street=""Pucka"" number=""10"" />
		<documents>
			<document type=""id"" number=""ABC102508"">
				<authority>
					<name>Urząd gminy w Terespolu</name>
					<autority-id>1234</autority-id>
				</authority>
				<valid-from>1999-20-20</valid-from>
				<valid-to>2009-20-20</valid-to>
			</document>
			<paszport type=""passport"" number=""999222"" />
		</documents>
	</person>
</people>

";
                var expected = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<people>
	<person name=""Piotr"" surname=""Rutkowski"">	
	</person>
	<person name=""Jan"" surname=""Kowalski"">
		<address street=""Mickiewicza"" number=""20"" />
		<address street=""Pucka"" number=""10"" />
		<documents>
			<document type=""id"" number=""ABC102508"">
				<authority>
					<name>Urząd gminy w Terespolu</name>
					<autority-id>1234</autority-id>
				</authority>
				<valid-from>1999-20-20</valid-from>
				<valid-to>2009-20-20</valid-to>
			</document>
			<document type=""passport"" number=""999222"" />
		</documents>
	</person>	
</people>

";
                actual.XmlShould().BeEqualTo(expected);

            });

            It should_fail = () => exception.Should().BeOfType<XmlAssertionException>();
            It should_have_a_proper_message = () => exception.Message.Should().Be(
                 ExceptionMessageExpected
                 );
        }

        [Subject("Xml asserts")]
        class when_comparing_differing_xml_trees_having_xml_attribute_value_unmatched
        {
            static Exception exception;

            static string ExceptionMessageExpected =
                @"Node //people[0]/person[1]/address[1]; Expected attribute [number] with value [10], but found [55]";

            Establish that = () => { };

            Because of = () => exception = Catch.Exception(() =>
            {
                var actual = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<people>
	<person name=""Piotr"" surname=""Rutkowski"">
	</person>
	<person name=""Jan"" surname=""Kowalski"">
		<address street=""Mickiewicza"" number=""20"" />
		<address street=""Pucka"" number=""55"" />
		<documents>
			<document type=""id"" number=""ABC102508"">
				<authority>
					<name>Urząd gminy w Terespolu</name>
					<autority-id>1234</autority-id>
				</authority>
				<valid-from>1999-20-20</valid-from>
				<valid-to>2009-20-20</valid-to>
			</document>
			<document type=""passport"" number=""999222"" />
		</documents>
	</person>
</people>
";
                var expected = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<people>
	<person name=""Piotr"" surname=""Rutkowski"">	
	</person>
	<person name=""Jan"" surname=""Kowalski"">
		<address street=""Mickiewicza"" number=""20"" />
		<address street=""Pucka"" number=""10"" />
		<documents>
			<document type=""id"" number=""ABC102508"">
				<authority>
					<name>Urząd gminy w Terespolu</name>
					<autority-id>1234</autority-id>
				</authority>
				<valid-from>1999-20-20</valid-from>
				<valid-to>2009-20-20</valid-to>
			</document>
			<document type=""passport"" number=""999222"" />
		</documents>
	</person>	
</people>

";
                actual.XmlShould().BeEqualTo(expected);

            });

            It should_fail = () => exception.Should().BeOfType<XmlAssertionException>();
            It should_have_a_proper_message = () => exception.Message.Should().Be(
                 ExceptionMessageExpected
                 );
        }

        [Subject("Xml asserts")]
        class when_comparing_differing_xml_trees_having_xml_attributes_set_unmatched
        {
            static Exception exception;

            static string ExceptionMessageExpected =
                @"Node //people[0]/person[1]/documents[2]/document[1]; Attributes collection does not match expected state, redundant attributes found: [valid-from]lacking attributes: [number]";

            Establish that = () => { };

            Because of = () => exception = Catch.Exception(() =>
            {
                var actual = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<people>
	<person name=""Piotr"" surname=""Rutkowski"">
	</person>
	<person name=""Jan"" surname=""Kowalski"">
		<address street=""Mickiewicza"" number=""20"" />
		<address street=""Pucka"" number=""10"" />
		<documents>
			<document type=""id"" number=""ABC102508"">
				<authority>
					<name>Urząd gminy w Terespolu</name>
					<autority-id>1234</autority-id>
				</authority>
				<valid-from>1999-20-20</valid-from>
				<valid-to>2009-20-20</valid-to>
			</document>
			<document type=""passport"" valid-from=""2000"" />
		</documents>
	</person>
</people>

";
                var expected = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<people>
	<person name=""Piotr"" surname=""Rutkowski"">	
	</person>
	<person name=""Jan"" surname=""Kowalski"">
		<address street=""Mickiewicza"" number=""20"" />
		<address street=""Pucka"" number=""10"" />
		<documents>
			<document type=""id"" number=""ABC102508"">
				<authority>
					<name>Urząd gminy w Terespolu</name>
					<autority-id>1234</autority-id>
				</authority>
				<valid-from>1999-20-20</valid-from>
				<valid-to>2009-20-20</valid-to>
			</document>
			<document type=""passport"" number=""999222"" />
		</documents>
	</person>	
</people>

";
                actual.XmlShould().BeEqualTo(expected);

            });

            It should_fail = () => exception.Should().BeOfType<XmlAssertionException>();
            It should_have_a_proper_message = () => exception.Message.Should().Be(
                 ExceptionMessageExpected
                 );
        }

        [Subject("Xml asserts")]
        class when_checking_for_attribute
        {
            static Exception exception;

            Establish that = () => { };

            Because of = () => exception = Catch.Exception(() =>
            {
                var actual = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<person name=""Piotr"" surname=""Rutkowski""></person>
";
                actual.XmlShould().HaveAttribute("surname", "nowak");

            });

            It should_fail = () => exception.Should().BeOfType<XmlAssertionException>();
            It should_have_a_proper_message = () => exception.Message.Should().Be(
                 "huj"
                 );

            //todo whhat about attribute case-insensitive check??
        }
    }
}
