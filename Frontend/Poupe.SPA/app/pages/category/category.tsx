import { useEffect, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TablePagination,
  Paper,
  TableFooter,
  Box,
  Grid,
  IconButton,
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import theme from "~/theme/theme";
import AddButton from "~/components/buttons/AddButton";
import AddCategory from "./category.add";
import DeleteCategory from "./category.delete";
import type { Category } from "~/domain/category/category.type";
import { translatePurpose } from "~/domain/mappers/mapper";

export default function CategoryPage(categories: Category[]) {
  const [openAddCategory, setOpenAddCategory] = useState(false);
  const [openDeleteCategory, setOpenDeleteCategory] = useState(false);
  const [selectedCategoryId, setSelectedCategoryId] = useState<string | null>(
    null
  );

  const [rows, setRows] = useState<Category[]>([]);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [rowsTotal, setRowsTotal] = useState(0);

  const handleDeleteCategory = (id: string) => {
    setSelectedCategoryId(id);
    setOpenDeleteCategory(true);
  };

  const handleChangePage = (_event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  useEffect(() => {
    setRows(categories ?? []);
    setRowsTotal(categories?.length ?? 0);
  }, []);

  return (
    <>
      <AddCategory
        open={openAddCategory}
        onClose={() => setOpenAddCategory(false)}
      />
      <DeleteCategory
        open={openDeleteCategory}
        onClose={() => setOpenDeleteCategory(false)}
        id={selectedCategoryId!}
      />
      <Grid
        container
        spacing={2}
        sx={{
          width: "80%",
          marginTop: "1rem",
          marginLeft: "auto",
          marginRight: "auto",
          justifyContent: "flex-end",
        }}
      >
        <AddButton onClick={() => setOpenAddCategory(true)} />
      </Grid>
      <Box
        sx={{
          width: "80%",
          marginTop: "2rem",
          marginLeft: "auto",
          marginRight: "auto",
        }}
      >
        <TableContainer
          component={Paper}
          sx={{ borderRadius: "1rem", boxShadow: theme.shadows[3] }}
        >
          <Table aria-label="agents table">
            <TableHead>
              <TableRow>
                <TableCell sx={{ textAlign: "center" }}>
                  Identificador
                </TableCell>
                <TableCell sx={{ textAlign: "center" }}>Descrição</TableCell>
                <TableCell sx={{ textAlign: "center" }}>Finalidade</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {categories?.map((row) => (
                <TableRow key={row.id}>
                  <TableCell align="center">{row.id}</TableCell>
                  <TableCell align="center">{row.description}</TableCell>
                  <TableCell
                    align="center"
                    sx={{
                      textTransform: "capitalize",
                      color:
                        row.purpose === "Income"
                          ? "success.main"
                          : row.purpose === "Expense"
                            ? "error.main"
                            : "warning.main",
                      fontWeight: 600,
                    }}
                  >
                    {translatePurpose(row.purpose)}
                  </TableCell>
                  <TableCell>
                    <IconButton
                      onClick={() => handleDeleteCategory(row.id)}
                      aria-label="Deletar"
                      title="Deletar"
                    >
                      <DeleteIcon color="error" />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>

            <TableFooter>
              {rows.length === 0 && (
                <TableRow>
                  <TableCell colSpan={3} />
                </TableRow>
              )}
            </TableFooter>
          </Table>
          <TablePagination
            rowsPerPageOptions={[3, 5, 10]}
            component="div"
            count={rowsTotal}
            rowsPerPage={rowsPerPage}
            page={page}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
            labelRowsPerPage="Resultados por página"
            labelDisplayedRows={({ from, to, count }) =>
              `${from}-${to} de ${count !== -1 ? count : `mais de ${to}`}`
            }
          />
        </TableContainer>
      </Box>
    </>
  );
}
