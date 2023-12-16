use Advising_System

SELECT * FROM dbo.FN_SemsterAvailableCourses('S23')
exec Procedures_ViewOptionalCourse 1, 'S23'
exec Procedures_ViewRequiredCourses 9, 'S24R2'
exec Procedures_ViewMS 9


select * from Student_Instructor_Course_take


-- for view available courses

select * from Course_Semester
select * from Semester

insert into Semester VALUES('W25', '2025-01-01', '2025-03-31')


INSERT INTO Course_Semester 
VALUES
    (1, 'W25'),
    (2, 'W25'),
    (3, 'W25'),
    (4, 'W25'),
    (5, 'W25'),
    (6, 'W25'),
    (7, 'W25'),
    (8, 'W25'),
    (9, 'W25'),
    (10, 'W25');


select * from Request

delete from Request where type = 'on'
delete from Request where request_id >= 15

select dbo.FN_StudentUpcoming_installment(1)
select dbo.FN_StudentUpcoming_installment(7)

exec Procedures_AdminIssueInstallment 1


select * from Student_Instructor_Course_take

update Student_Instructor_Course_take
set instructor_id = null
where instructor_id is not null

select * from view_Course_prerequisites

select * from Exam_Student

select * from MakeUp_Exam

select * from Student

SELECT * FROM Students_Courses_transcript