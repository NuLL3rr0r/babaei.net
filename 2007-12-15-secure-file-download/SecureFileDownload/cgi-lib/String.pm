package String;

# $Id: String.pm,v 1.7 2003/07/23 03:50:50 sherzodr Exp $

use strict;
use Carp;
use vars qw($VERSION);
use overload (
    '""'     => 'asString',
    "+"      => "add",
    fallback => 'asString',
  );

$VERSION = '1.5';

# Preloaded methods go here.


sub DESTROY {
  my $string = shift;
  $$string = undef;

}




sub new {
  my $class = shift;
  $class = ref($class) || $class;
  my $string = shift;
  if ( ref($string) && (ref($string) eq 'SCALAR') ) {
    $string = $$string;
  } elsif ( ref($string) ) {
    croak "$$string doesn't look like a string";
  }

  return bless (\$string, $class);
}


sub length {
  my $string = shift;
  return length ($$string);
}



sub charAt {
  my ($string, $n) = @_;
  unless ( defined($n) ) {
    croak "Usage: chartAt(n)";
  }
  return substr ($$string, $n, 1);
}



*as_string = \&asString;
sub asString {
  my $string = shift;
  return $$string;
}


*append = \&concat;
sub concat {
  my $string = shift;

  my $newStr = join ("", $$string, @_);
  return $string->new($newStr)
}


sub prepend {
    my $string = shift;
    my $newStr = join("", @_, $$string);
    return $string->new($newStr)
}

sub add {
   my ($string_obj, $string2, $bool) = @_;

   if ( $bool ) {
       return $string_obj->prepend($string2)
   }
   return $string_obj->concat($string2)
}


*index = \&indexOf;
sub indexOf {
  my ($string, $substring, $start) = @_;

  return CORE::index($$string, $substring, $start||0)
}


sub toUpperCase {
  my $string = shift;

  return $string->new(uc($$string))
}


sub toLowerCase {
  my $string = shift;

  return $string->new(lc($$string))
}


sub split {
  my ($string, $delim, $limit) = @_;

  my @rv =  CORE::split(/$delim/, $$string);
  if ( defined $limit ) {
    @rv = CORE::splice(@rv, $limit);
  }
  map { $string->new($_) } @rv
}


sub eq {
  my ($string, $string2) = @_;

  return ($$string eq $string2)
}


sub serialize {
  my $string = shift;

  eval "require Storable";
  if ( $@ ) {
    croak "serialize is not supported: Storable is missing";
  }
  return Storable::freeze($string)
}




sub match {
  my ($string, $pattern) = @_;

  unless ( defined $pattern ) {
    croak "Usage: STRING->match(PATTERN)";
  }
  my @rv = map { $string->new($_) } $$string =~ m/($pattern)/g;
  unless ( defined $rv[0] ) {
      return undef;
  }
  return \@rv
}



1;
__END__
# Below is stub documentation for your module. You better edit it!

=head1 NAME

String - Perl extension representing string object

=head1 SYNOPSIS

  use String;

  my $str = new String("Perl");

  print "The string is '$str'\n";
  printf("Length of the string is %d characters\n", $str->length);
  printf("The first character of the string is %s\n", $str->charAt(0));

  my $pos = $str->indexOf('er');
  if (  $pos != -1 ) {
    printf("String '%s' contains 'er' at position %d\n", $str, $pos);
  }

  my $newStr = $str->concat(" ", "Guru");
  printf("Length of the new string('%s') is %d characters\n", $newStr, $newStr->length);

=head1 DESCRIPTION

String is a Perl 5 class representing a string object. It provides String methods for manipulating
text, such as match(), length(), charAt(), indexOf(), split(), asString() and several
more. Since Perl already provides built-in utilities for manipulating strings, each METHOD description
also provides how this particular task is implemented internally. This may be useful for comparison.

=head1 CONSTRUCTOR

=over 4

=item new(STRONG)

takes a STRING as an argument, and returns String object. Argument passed to new() - STRING
can be either a string, a variable interpolating into a string or a reference to a string.
If it receives anything else, will through an exception. Example:

  $str = new String("Perl");

Internally String object is represented as reference to the STRING (passed as the first argument).

  Note: examples throughout the manual will be working on this particular String
  object unless noted otherwise.

=back

=head1 METHODS

=over 4

=item C<length()>

returns the length of the string. Internally calls Perl's C<length()> built-in function.
Example:

  # assuming 'Perl' is a string
  $str->length == 4;

=item C<charAt(n)>

takes a digit as the first argument, and returns the character from that position of the string.
Example:

  # getting the first character
  $str->charAt(0) == 'P';

  # getting the last character
  $str->charAt( $str->length-1 ) == 'l';

If the argument passed to charAt() is negative, starts the indexing from the end of the string

  # getting the last character of the string:
  $str->charAt(-1);

This function is internally implemented by substr().

=item indexOf('substring')

returns the index of 'substring' from within String object. Internally calls Perl's built-in
index() function. Example:

  $str->indexOf('er') == 1;

This example returns 1, because the first (and the only in our case) occasion of
substring "er" starts at the second character of "Perl". Since indexing starts at 0,
the second character's index is actually 1. If the match doesn't occur, the return value
is -1. Example:

  if ( $str->indexOf('ear') == -1 ) {
    print "There is no 'ear' in '$str'\n";
  }

=item match(PATTERN)

Accepts a regular expression pattern, and returns a reference to an array on success, undef otherwise.
First element of the returned array is whatever matched. Capturing paranthesis, if any used in
the PATTERN, will constitute other elements of the array. Following example is retrieving an e-mail
address from a string and capturing username and host parts of the address:

  $email = new String('Sherzod Ruzmetov <sherzodr@cpan.org>');
  $result = $email->match('([\w.-]+)@([\w-]+\.[\w.-]+)');
  if ( $result ) {
    printf("Out of '%s', I could match 's%'\n", $string, $result->[0]);
    printf("Username is '%s'\n", $result->[1]);
    printf("Host address is '%s'\n", $result->[2]);

Note: all the elements of the $result set are also of String type. So the following applies
for the above $result:

  if ( $result->[2]->match('^cpan\.org$') ) {
    print "Oh, is that a CPAN e-mail account? Neat!\n";
  }

=item asString()

Returns the original string. Due to overloading, whenever you use String object in the context
where string is expected, asString() will be called for you automatically. Example:

  printf("Original string is '%s'\n", $str->asString );  
  print "You could also print it this way: '$str'\n";

=item eq(STRING)

for comparing the string to another string. The argument STRING can be either a literal string,
or String object:

  if ( $str->eq('Java') ) {
    print "They are the same\n";
  }

Due to overloading, you can also use Perl's built-in C<eq> operator to compare
String object to another String, or even String object to another "string":

  if ( $str eq 'Java') {
    print "No way!?\n";
  }
  # or
  my $str2 = new String("Perl");
  if ( $str eq $str2 ) {
    print "That's more like it.\n";
  }

This method is internally implemented with Perl's built-in C<eq> operator.

=item toLowerCase()

returns a String object by converting all the characters of the original string to their lower cases.
Internally implemented with Perl's built-in C<uc()> function:

  $str->toLowerCase() eq 'perl';

=item toUpperCase()

the same as toLowerCase(), but returns String object by upper-casing the its characters. Implemented
with Perl's built-in C<lc()> function

=item concat(LIST)

returns a String object by concatenating all the elements of LIST. String itself is not modified.
Example:

  $str = new String("Hello");
  $newStr = $str->concat(" ", "World");
  $newStr eq "Hello World";
  $str    eq "Hello";

You can still keep using Perl's built-in concatination operator, C<.> (dot):

  $str = new String("Hello");
  $str .= " World";
  $str eq "Hello World";

Notice the differences between the above two examples; the first one returns a $newStr -
a new string object and doesn't modify the original $str. The second example doesn't
return/create any additional objects, but modifies the original $str.

You can also use C<+> (plus) operator for concatinating string objects with another string,
just like in C++ and Java:

  $str = new String("Hello");
  $str += " World"
  $str eq "Hello World";

If you don't want to change the original $str:

  $str = new String("Hello");
  $newStr = $str + " World";
  $newStr eq "Hello World";
  $str    eq "Hello";

=item append(LIST)

Same as C<concat()>

=item prepend(LIST)

Similar to C<concat()>, but concatinates the string(s) to the front of the original
string as returns an appropriate String object. Original string is not modified.

=item split(PATTERN [,LIMIT])

splits a string into substrings at each PATTERN, and returns an array of respective
String objects. If LIMIT is defined, it will restrict the length of an array to first LIMIT
elements. Example:

  $str = new String("one, two, three, four, five");
  @array = $str->split(',\s*');
  for ( @array ) {
    print "$_\n";
  }

=item serialize()

returns a serialized String object. Internally uses L<Storable> to serialize the object
into a string. If the Sorable.pm is not available on your machine, you'll get an exception.

=back

=head1 TODO

I do not expect this class to be exhaustive nor comprehensive. I'm open to suggestions,
patches and comments.

=head1 AUTHOR

Sherzod B. Ruzmetov, E<lt>sherzodr@cpan.orgE<gt>

=head1 SEE ALSO

L<String::Approx>.

=cut
