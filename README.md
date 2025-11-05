# Old Phone Keypad Decoder

Converts old phone keypad input to text, like how we used to text on Nokia phones.

## Quick Start

```bash
dotnet build
dotnet test
dotnet run --project src/OldPhonePad.csproj
```

## How It Works

Remember T9 texting? Press a number key multiple times to get different letters.

**Keypad:**
```
1        2(ABC)   3(DEF)
4(GHI)   5(JKL)   6(MNO)
7(PQRS)  8(TUV)   9(WXYZ)
         0(space)
```

**Rules:**
- Press 2 once = A, twice = B, three times = C
- Need two letters from same key? Add a space: `2 2` = AA
- Made a mistake? Hit `*` to delete
- Done typing? End with `#`

**Examples:**
```
33#                      → E
227*#                    → B (typed "AA" then deleted one)
4433555 555666#          → HELLO
8 88777444666*664#       → TURING
```

## Code Usage

```csharp
using IronSoftware.PhoneKeypad;

string message = OldPhonePad.Decode("4433555 555666#");
Console.WriteLine(message);  // HELLO
```

## Project Layout

```
src/
  ├── OldPhonePad.cs       - Main decoder logic
  └── Program.cs           - Demo app with examples

tests/
  └── OldPhonePad.Tests/
      └── OldPhonePadTests.cs  - 38 test cases

OldPhonePad.sln            - Visual Studio solution
```

## How The Decoder Works

Pretty straightforward:
1. Read through the input character by character
2. Group up consecutive same digits (like "222")
3. Convert each group to a letter (3 presses of 2 = C)
4. Handle special stuff (spaces, backspace, end)

The trick is using modulo to wrap around. Key 2 has "ABC", so:
- 2 % 3 = 0 → A
- 22 % 3 = 1 → B
- 222 % 3 = 2 → C
- 2222 % 3 = 0 → back to A

## Error Handling

The decoder checks for common mistakes:
- Input can't be empty
- Must end with `#`
- Only accepts digits, spaces, `*`, and `#`

Throws exceptions if something's wrong, with a message explaining what.

## Tests

38 tests covering everything:
- Basic typing (single and multiple keys)
- Pausing between same letters
- Backspace
- Edge cases (empty backspace, wrapping, etc)
- Error conditions

Run `dotnet test` - all green.

## Built With

- .NET 8.0
- xUnit for testing
- That's it, no external dependencies

## Notes

Made with help from Claude Code. Check `AI_PROMPT.md` for details on what I did vs what AI did.
# IronSoftware-TestChallenge
