import { styled, Paper, TextField, TableCell, TableRow, PaginationItem, Select } from "@mui/material";


export const PaperStyle = styled(Paper)(({ theme }) => ({
    width: "100%",
    borderRadius: "32px",
}));

export const TextFieldStyle = styled(TextField)(({ theme }) => ({
    paddingTop: "24px",
    paddingBottom: "15px",
    width: "30%",
    "& fieldset": {
        borderRadius: "15px",
        borderColor: "#7E7E7E",
    },
    InputLabelProps: {
        color: theme.palette.mode != "dark" ? "#7E7E7E" : "#fff",
    },
    inputProps: {
        height: "58px",
        paddingTop: "0px",
        paddingBottom: "0px",
        fontSize: "18px"
    },
    "& .MuiOutlinedInput-root": {
        color: theme.palette.mode != "dark" ? "#7E7E7E" : "#fff",
        "& fieldset": {
            borderColor: "#AEAEAE"
        }
    }
}));

export const TableCellStyle = styled(TableCell)(({ theme }) => ({
    borderColor: "transparent",
    padding: "8px"
}));

export const SelectStyle = styled(Select)(({ theme }) => ({
    height: "30px",
    borderRadius: "18px"
}));

export const PaginationItemStyle = styled(PaginationItem)(({ theme }) => ({
    borderRadius: "12px",
    "&.Mui-selected": {
        color: "white",
        backgroundColor: `${theme.palette.secondary.main}`,
        "&:hover": {
            backgroundColor: `${theme.palette.secondary.dark}`,
        },
    }
}));
