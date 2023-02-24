# rws_example
Interview example for rws.

1. Please find at least 5 potential code issues an be able to explain the reason behind it.
2. Refactor the app to allow:
    - Work with documents of various storages eg. filesystem, cloud storage or HTTP (HTML read-only) etc. Implement just one of them but be sure that implementation is versatile for adding other storages.
    - Be capable of reading/writing different formats. Implement XML and JSON format, but be sure that implementation is versatile for adding more formats (YAML, BSON, etc.).
    - Build the app in the way to be able to test classes in isolation.
    - Be able to add new formats and storages in the future so it will have none or minimal impact on the existing code.
    - Be able to use any combination of input/output storages and formats (eg. read JSON from filesystem, convert to XML and upload to cloud storage)

## Potential code issues

1. Code as provided cannot be compiled

    Variable **input** as defined is valid only inside the try block. It's not possible to use it after block.

    ```cs
    try
    {
        FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
        var reader = new StreamReader(sourceStream);
        string input = reader.ReadToEnd();
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
    var xdoc = XDocument.Parse(input);
    ```

    Of course, it could be just an oversight by the author of the example, an error in the input, but in any case the code does not work and the broken code should not be committed to repo, or the CI build should not allow to merge the pullrequest.

2. Wrong way to re-thow an exemption

    In practice, there may be good reasons why it is necessary to re-throw the exception. For example, we will catch all exceptions and make a decision in the catch block about further processing based on the specific exception type.

    ```cs
    // not good example, call stack is modified and information are lost
    throw new Exception(ex.Message);
    throw ex;

    // prefered way - original exception is thrown
    throw;
    ```

    User-filtered exception handlers introduced in c# 6 (catch/when statement) provide an elegant way to reduce the need for branching exception handling and rethrow logic.

3. Constant strings are hardcoded in code

    Understandably, for the sake of simplicity and brevity of the example, strings such as **title** and **text** are included directly in the code.  But in production code, such strings or magic constants should be stored in constants. If they are global values, they should be placed in the global/ domain  project otherwise at the top of the file. Same situation for sourceFileName and targetFileName which should be in production code input paramters or constants.

4. Missing Using statement or stream closing

    In general, when working with an object that implements the IDisposable (here 2 x FileStream, StreamReader and StreamWriter) interface, we should ensure that the Close / Dispose method is called correctly, preferably via the Using. Calling Close / Dispose frees resources and in the case of StreamWriter ensures that the **Flush** method is called and the entire content of the buffer is written to the output.

5. Not handling possible exception while parsing xml document

    Code is expecting that input is valid xml document and that it contains elements title and text under root element. In production code there should be implemented some kind of validation / exception handling and a convenient and understandable error message should be given to user.

6. Not handling possible exception while writing output file

    Code is expecting that there is **not existing** target file and that it will be possible to crate new one. `` File.Open(targetFileName, FileMode.Create, FileAccess.Write) ``
    In production code there should be implemented some kind of validation / exception handling and a convenient and understandable error message should be given to user.

7. Comments and project layout.

    Obviously, in the example, for the sake of simplicity and brevity, all classes are in one file. However, the following requirements should be met in the production code:

    - Public classes and their public items are supposed to have comments but the Document class has none.

    - The Document class is a business object class and should be placed in the business object project or at least in a different namespace and in a separate file.

    The program should read some data source (file, ..), convert from one format to another format and save the data. For maintainability and extensibility it would be useful to change the structure of the solution. One of many possible structure:
    - Moravia.Domain
        - Business objects like Document
        - Interface definition
            - IStorageProvider with methods Save, Read methods.
            - IFormatProvider with methods LoadDocumentFromFormat, SaveDocumentToFormat
    - Moravia.StorageProviders (implementations of IStorageProvider)
    - Moravia.FormatProviders (implementations of IFormatProvider)
    - Moravia.Console
    - Moravia.Tests
