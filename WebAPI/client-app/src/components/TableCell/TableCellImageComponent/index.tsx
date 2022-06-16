import Box from "@mui/material/Box"
import TableCell from "@mui/material/TableCell"
import Typography from "@mui/material/Typography"

import { FC } from "react"
import { ITableCellImage } from "./type"

const TableCellImageComponent: FC<ITableCellImage> = ({ label, image }) => {
    return (
        <TableCell
            sx={{
                display: "flex",
                alignItems: "center"
            }}>
            <Box sx={{
                backgroundImage: `url(` + image + `)`,
                backgroundPosition: "center center",
                backgroundSize: "cover",
                display: "flex",
                width: "80px",
                height: "80px",
                borderRadius: "8px",
            }}>
            </Box>
            <Box>
                <Typography
                    sx={{
                        ml: 1
                    }}
                    variant="h6"
                    component="h6">
                    {label}
                </Typography>
            </Box>
        </TableCell>
    )
}

export default TableCellImageComponent;