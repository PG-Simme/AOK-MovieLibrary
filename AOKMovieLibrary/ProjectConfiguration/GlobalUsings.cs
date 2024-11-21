// Project: AOKMovieLibrary

global using AOKMovieLibrary.Abstractions;
global using AOKMovieLibrary.Implementations;
global using AOKMovieLibrary.Models.Commands;
global using AOKMovieLibrary.Models.DAL;
global using AOKMovieLibrary.Models.Responses;
global using AOKMovieLibrary.Models.ViewModels;
global using AOKMovieLibrary.ProjectConfiguration;

// Microsoft

global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
global using Microsoft.Extensions.DependencyInjection;
global using System.ComponentModel.DataAnnotations;

// Misc
global using FluentValidation;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AOKMovieLibrary.Tests")]