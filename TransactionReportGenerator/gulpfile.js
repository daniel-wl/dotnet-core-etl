"use strict";

var gulp = require('gulp');
var exec = require('gulp-exec');

var dataFile = './Data.csv';

gulp.task('copyData', function(){
    gulp.src(dataFile)
    .pipe(gulp.dest('./bin/Debug/netcoreapp2.0'));
});

gulp.task('build', function(){
    exec('dotnet build', function(err, stdout, stderr){
        if(err){
            console.log(stderr);
        } else {
            console.log(stdout);
        }
    });
});

gulp.task('run', ['copyData', 'build'], function(){
    exec('dotnet run', function(err, stdout, stderr){
        if(err){
            console.log(stderr);
        } else {
            console.log(stdout);
        }
    });
})

gulp.task('default', ['run']);