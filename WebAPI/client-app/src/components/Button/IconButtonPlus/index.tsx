import IconButton from "@mui/material/IconButton";

import { FC, } from "react";

import { white_plus } from "../../../assets/icons";

interface Props {
    onClick: () => void
}

const IconButtonPlus: FC<Props> = ({ onClick }) => {
    return (
        <IconButton
            size="large"
            color="inherit"
            onClick={onClick}
            sx={{
                borderRadius: '12px',
                background: "#F45626",
                "&:hover": {
                    background: "#CB2525"
                },
                "&& .MuiTouchRipple-child": {
                    backgroundColor: "transparent"
                }
            }}
        >
            <img
                style={{ width: "30px" }}
                src={white_plus}
                alt="icon"
            />
        </IconButton>
    )
}

export default IconButtonPlus;