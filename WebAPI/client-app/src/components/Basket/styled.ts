import { TextField, styled } from '@mui/material';

export const TextFieldStyle = styled(TextField)(({ theme }) => ({
    "& .MuiInputLabel-root": {
        padding: "0px",
        color: "#45a29e",
        "&.Mui-focused": {
            color: "#45a29e"
        },
        "&.Mui-error ": {
            color: "#d32f2f"
        },
        "&.Mui-disabled": {
            color: "#f1f1f1"
        },
    },
    "& .MuiOutlinedInput-root": {
        color: theme.palette.mode != "dark" ? "#777" : "#fff",
        "&>*:nth-of-type(1)": {
            padding: "2.5px 8.5px",
            fontSize: "12px",
            textAlign: "center"
        },
        "& fieldset": {
            borderRadius: "5px",
            border: theme.palette.mode != "dark" ? "1px solid black" : "1px solid white"
        },
        "&:hover fieldset": {
            borderRadius: "5px",
            border: theme.palette.mode != "dark" ? "1px solid black" : "1px solid white"
        },
        "&.Mui-focused fieldset": {
            borderRadius: "5px",
            border: theme.palette.mode != "dark" ? "1px solid black" : "1px solid white"
        }
    },
}));