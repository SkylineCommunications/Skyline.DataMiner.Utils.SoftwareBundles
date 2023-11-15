# SoftwareBundles

## About

SoftwareBundles is a class library to manage custom-created software bundles. The software bundle will contain installation files (e.g. .deb and .rpm files) as well as a JSON file to describe the contents and compatibility of the software bundle.

## Usage

A SoftwareBundle object can be created from a zipped file with the appropriate installation files and JSON contents. 
The library contains useful methods to allow:
* Unzipping and zipping the software bundle file
* Return full paths of the installation files within
* Return the info of the software bundle; associated software, supported OS, version, dependencies etc.
