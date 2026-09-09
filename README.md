# Language Catalogue

A C# console coursework project for querying a pipe-delimited language catalogue. Commands list languages by group, country and population, display individual entries and calculate population totals.

The original data-loading scaffold was supplied as part of the assignment. The existing license is retained in `LICENSE.txt`.

## Run

Build `MJU23v_DTP_T1.sln`, then run the application from its build-output directory so the relative path to `lang.txt` resolves. Enter `help` to list commands and `quit` to exit.

The project targets .NET 6. It is an early exercise: input handling assumes an interactive console and the file parser expects correctly formatted rows.
