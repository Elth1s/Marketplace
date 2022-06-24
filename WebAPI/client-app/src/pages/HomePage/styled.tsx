import { ListItemButton, Button, styled } from "@mui/material";

export const ListItemButtonStyle = styled(ListItemButton)(({ theme }) => ({
    width: "100%",
    padding: "0px",
    paddingBottom: "21px",
    "&:hover": {
        background: "transparent"
    },
    "&& .MuiTouchRipple-child": {
        backgroundColor: "transparent"
    }
}));

export const ButtonNoveltyStyle = styled(Button)(({ theme }) => ({
    height: "45px",
    fontSize: "24px",
    borderRadius: "9px",
    borderWidth: "1.7px",
    "&:hover": {
        borderWidth: "1.7px",
    },
    borderColor: `${theme.palette.primary.main}`,
    textTransform: "none",
    justifyContent: "space-between",
    paddingLeft: "21px",
    paddingRight: "20px",
}));