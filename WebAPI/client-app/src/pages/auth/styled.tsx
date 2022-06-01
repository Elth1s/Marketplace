import Typography, { TypographyProps } from '@mui/material/Typography';
import Avatar, { AvatarProps } from '@mui/material/Avatar';
import TextField, { TextFieldProps } from '@mui/material/TextField';
import LoadingButton, { LoadingButtonProps } from '@mui/lab/LoadingButton';
import { styled } from '@mui/material/styles';

export const AuthHeaderTypography = styled(Typography)<TypographyProps>(({ theme }) => ({
    height: "60px",
    fontSize: "45px",
    //fontFamily: "",
}));

export const AuthSideTypography = styled(Typography)<any>(({ theme }) => ({
    fontSize: "20px",
    //fontFamily: "",
}));

export const AuthLoadingButton = styled(LoadingButton)<LoadingButtonProps>(({ theme }) => ({
    width: "100%",
    fontSize: "30px",
    borderRadius: "0px",
    type: "submit",
}));

export const AuthAvatar = styled(Avatar)<AvatarProps>(({ theme }) => ({
    width: "55px",
    height: "55px",
    background: "#000",
    color: "#fff"
}));

export const AuthTextField = styled(TextField)<TextFieldProps>(({ theme }) => ({
    "& .MuiInputLabel-root": {
        color: "#000",
        "&.Mui-focused": {
            color: theme.palette.secondary.main
        },
        "&.Mui-error ": {
            color: theme.palette.error.main
        },
    },
    "& .MuiInput-underline:before": {
        borderBottom: "2px solid #000"
    },
    "& .MuiInput-underline:after": {
        borderBottom: `2px solid ${theme.palette.secondary.main}`
    },
    "& .MuiInput-underline:hover:before": {
        borderBottom: "2px solid blue"
    },
    "& .MuiIconButton-root": {
        color: "#000",
    },
}));


    // "& .MuiInputLabel-root": {
    //     color: "#000",
    //     "&.Mui-focused": {
    //         color: theme.palette.secondary.main
    //     },
    //     "&.Mui-error ": {
    //         color: theme.palette.error.main
    //     },
    //     "&.Mui-disabled": {
    //         color: "#f1f1f1"
    //     },
    // },
    // "& .MuiInput-underline": {
    //     borderbottom: `2px solid #000`
    // },
    // "& .MuiInput-underline:after": {
    //     borderBottom: `2px solid ${theme.palette.secondary.main}`
    // },
// "& fieldset": {
// },
// "&:hover fieldset": {
//     border: `2px solid ${theme.palette.secondary.main}`
// },
// "&.Mui-focused fieldset": {
//     border: `2px solid ${theme.palette.secondary.main}`
// },
// "&.Mui-disabled fieldset": {
//     border: `2px solid #f1f1f1`
// },
// "&.Mui-error fieldset": {
//     border: `2px solid ${theme.palette.error.main}`
// },
// "&.Mui-error:hover fieldset": {
//     border: `2px solid ${theme.palette.error.main}`
// }

// color: "#000",
//     '& .MuiInput-input': {
//     '&:hover, &.Mui-focusVisible': {
//         boxShadow: `0px 0px 0px 8px ${alpha(theme.palette.success.main, 0.16)}`,
//         },
//     '&.Mui-active': {
//         boxShadow: `0px 0px 0px 14px ${alpha(theme.palette.success.main, 0.16)}`,
//         },
// },
// 
