

import { AddBox, Create, Edit } from "@mui/icons-material";
import { Avatar, Button, Checkbox, IconButton, TableRow } from "@mui/material";
import { useEffect, useState } from "react";
import EnhancedTable from "../../../../components/EnhancedTable";
import { TableCellStyle } from "../../../../components/EnhancedTable/styled";
import LinkRouter from "../../../../components/LinkRouter";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { HeadCell } from "../../../../store/types";
import { ICategoryInfo } from "../types";


const headCells: HeadCell<ICategoryInfo>[] = [
    {
        id: 'id',
        numeric: true,
        label: 'Identifier',
    },
    {
        id: 'name',
        numeric: false,
        label: 'Name',
    },
    {
        id: 'urlSlug',
        numeric: false,
        label: 'Url slug',
    },
    {
        id: 'image',
        numeric: false,
        label: 'Image',
    },
    {
        id: 'icon',
        numeric: false,
        label: 'Icon',
    },
    {
        id: 'parentName',
        numeric: false,
        label: 'Parent category',
    }
];

const CategoryTable = () => {
    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof ICategoryInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { SearchCategories, DeleteCategories } = useActions();
    const { categories, count } = useTypedSelector((store) => store.category);

    useEffect(() => {
        document.title = "Categories";
        getData();
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await SearchCategories(page, rowsPerPage, name, isAscOrder, orderBy);
            setSelected([]);
        } catch (ex) {
        }
    };

    const onDelete = async () => {
        await DeleteCategories(selected);
        setPage(1);
        getData();
    }

    const handleClick = (event: React.MouseEvent<unknown>, id: number) => {
        const selectedIndex = selected.indexOf(id);
        let newSelected: readonly number[] = [];

        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, id);
        } else if (selectedIndex === 0) {
            newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
            newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelected = newSelected.concat(
                selected.slice(0, selectedIndex),
                selected.slice(selectedIndex + 1),
            );
        }

        setSelected(newSelected);
    };

    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (selected.length == 0) {
            const newSelecteds = categories.map((n) => n.id);
            setSelected(newSelecteds);
            return;
        }
        setSelected([]);
    };

    const isSelected = (id: number) => selected.indexOf(id) !== -1;

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(1);
    };

    return (
        <>
            <LinkRouter underline="none" color="unset" to="/admin/category/create" >
                <Button
                    variant="contained"
                    sx={{
                        my: 2,
                        px: 4,
                    }}
                >
                    Create
                </Button>
            </LinkRouter>
            <EnhancedTable
                page={page}
                rowsPerPage={rowsPerPage}
                handleChangePage={handleChangePage}
                handleChangeRowsPerPage={handleChangeRowsPerPage}
                setName={setName}
                handleSelectAllClick={handleSelectAllClick}
                isAscOrder={isAscOrder}
                setIsAscOrder={setIsAscOrder}
                orderBy={orderBy}
                setOrderBy={setOrderBy}
                headCells={headCells}
                numSelected={selected.length}
                count={count}
                onDelete={onDelete}
                update={
                    <LinkRouter underline="none" color="secondary" to={`/admin/category/update/${selected[selected.length - 1]}`} >
                        <Edit />
                    </LinkRouter>
                }
                tableBody={
                    categories.map((row, index) => {
                        const isItemSelected = isSelected(row.id);
                        const labelId = `enhanced-table-checkbox-${index}`;

                        return (
                            <TableRow
                                hover
                                onClick={(event) => handleClick(event, row.id)}
                                role="checkbox"
                                aria-checked={isItemSelected}
                                tabIndex={-1}
                                key={row.id}
                                selected={isItemSelected}
                            >
                                <TableCellStyle sx={{ paddingLeft: "11px" }} padding="checkbox">
                                    <Checkbox
                                        color="primary"
                                        sx={{ borderRadius: "12px" }}
                                        checked={isItemSelected}
                                        inputProps={{
                                            'aria-labelledby': labelId,
                                        }}
                                    />
                                </TableCellStyle>
                                <TableCellStyle
                                    component="th"
                                    id={labelId}
                                    scope="row"
                                >
                                    {row.id}
                                </TableCellStyle>
                                <TableCellStyle align="center">{row.name}</TableCellStyle>
                                <TableCellStyle align="center">{row.urlSlug}</TableCellStyle>
                                <TableCellStyle align="center">
                                    <Avatar
                                        sx={{ mx: "auto" }}
                                        src={row.image}
                                        alt="Image"
                                    />
                                </TableCellStyle>
                                <TableCellStyle align="center">
                                    <Avatar
                                        sx={{ mx: "auto" }}
                                        src={row.icon}
                                        alt="Icon"
                                    />
                                </TableCellStyle>
                                <TableCellStyle align="center">{row.parentName}</TableCellStyle>
                            </TableRow>
                        );
                    })
                }
            />
        </>
    );
}

export default CategoryTable;