import * as Yup from 'yup';

export const LogInSchema = Yup.object().shape({
    emailOrPhone: Yup.string().required().label('Email or phone'),
    password: Yup.string().required().label('Password')
});

const passwordRegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}$/

export const SignUpSchema = Yup.object().shape({
    firstName: Yup.string().min(3).max(50).required().label('First Name'),
    secondName: Yup.string().min(3).max(75).required().label('Second Name'),
    emailOrPhone: Yup.string().required().label('Email or phone'),
    password: Yup.string().matches(passwordRegExp, 'Password is not valid').required().label('Password'),
});