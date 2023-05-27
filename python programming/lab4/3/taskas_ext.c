#include <Python.h>

static PyObject *TaskasError;

// x ir y struktura
typedef struct {
    PyObject_HEAD
    PyObject *x;
    PyObject *y;
} Taskas;

// __init__
static int
Taskas_init(Taskas *self, PyObject *args, PyObject *kwds)
{
    PyObject *x=NULL, *y=NULL, *tmp;

    if (!PyArg_ParseTuple(args, "OO", &x, &y)) {
        return -1;
    }

    tmp = self->x;
    Py_INCREF(x);
    self->x = x;
    Py_XDECREF(tmp);

    tmp = self->y;
    Py_INCREF(y);
    self->y = y;
    Py_XDECREF(tmp);

    return 0;
}

// x getter
static PyObject *
Taskas_getx(Taskas *self, void *closure)
{
    Py_INCREF(self->x);
    return self->x;
}

// x setter
static int
Taskas_setx(Taskas *self, PyObject *value, void *closure)
{
    if (value == NULL) {
        PyErr_SetString(TaskasError, "Cannot delete the x attribute");
        return -1;
    }

    if (!PyLong_Check(value)) {
        PyErr_SetString(TaskasError, "The x attribute value must be an integer");
        return -1;
    }

    Py_INCREF(value);
    Py_XDECREF(self->x);
    self->x = value;

    return 0;
}

// y getter
static PyObject *
Taskas_gety(Taskas *self, void *closure)
{
    Py_INCREF(self->y);
    return self->y;
}

// y setter
static int
Taskas_sety(Taskas *self, PyObject *value, void *closure)
{
    if (value == NULL) {
        PyErr_SetString(TaskasError, "Cannot delete the y attribute");
        return -1;
    }

    if (!PyLong_Check(value)) {
        PyErr_SetString(TaskasError, "The y attribute value must be an integer");
        return -1;
    }

    Py_INCREF(value);
    Py_XDECREF(self->y);
    self->y = value;

    return 0;
}

// move metodas
static PyObject *
Taskas_move(Taskas *self, PyObject *args)
{
    PyObject *x_offset, *y_offset, *new_x, *new_y;

    if (!PyArg_ParseTuple(args, "OO", &x_offset, &y_offset)) {
        return NULL;
    }

    new_x = PyNumber_Add(self->x, x_offset);
    new_y = PyNumber_Add(self->y, y_offset);

    if (new_x == NULL || new_y == NULL) {
        Py_XDECREF(new_x);
        Py_XDECREF(new_y);
        return NULL;
    }

    Py_DECREF(self->x);
    Py_DECREF(self->y);
    self->x = new_x;
    self->y = new_y;

    Py_RETURN_NONE;
}

// metodu lentele
static PyMethodDef Taskas_methods[] = {
    {"move", (PyCFunction)Taskas_move, METH_VARARGS,
     "Move the point by the given offsets"
    },
    {NULL}  /* Sentinel */
};

// getteriai ir setteriai
static PyGetSetDef Taskas_getseters[] = {
    {"x",
     (getter)Taskas_getx, (setter)Taskas_setx,
     "x coordinate",
     NULL},
    {"y",
     (getter)Taskas_gety, (setter)Taskas_sety,
     "y coordinate",
     NULL},
    {NULL}  /* Sentinel */
};

// atminties saugojimas sunaikinus objekta
static void
Taskas_dealloc(Taskas* self)
{
    Py_XDECREF(self->x);
    Py_XDECREF(self->y);
    Py_TYPE(self)->tp_free((PyObject*) self);
}

// tipas
static PyTypeObject TaskasType = {
    PyVarObject_HEAD_INIT(NULL, 0)
    .tp_name = "taskas_ext.Taskas",
    .tp_doc = "Tasko tipas",
    .tp_basicsize = sizeof(Taskas),
    .tp_itemsize = 0,
    .tp_flags = Py_TPFLAGS_DEFAULT | Py_TPFLAGS_BASETYPE,
    .tp_new = PyType_GenericNew,
    .tp_init = (initproc) Taskas_init,
    .tp_dealloc = (destructor) Taskas_dealloc,
    .tp_methods = Taskas_methods,
    .tp_getset = Taskas_getseters,
};

// modulis
static PyModuleDef taskas_ext_module = {
    PyModuleDef_HEAD_INIT,
    .m_name = "taskas_ext",
    .m_doc = "Tasko tipo modulis",
    .m_size = -1,
};

// prapletimas
PyMODINIT_FUNC
PyInit_taskas_ext(void)
{
    PyObject* m;

    if (PyType_Ready(&TaskasType) < 0)
        return NULL;

    m = PyModule_Create(&taskas_ext_module);
    if (m == NULL)
        return NULL;

    TaskasError = PyErr_NewException("taskas_ext.error", NULL, NULL);
    Py_XINCREF(TaskasError);
    if (PyModule_AddObject(m, "error", TaskasError) < 0) {
        Py_XDECREF(TaskasError);
        Py_CLEAR(TaskasError);
        Py_DECREF(m);
        return NULL;
    }

    Py_INCREF(&TaskasType);
    if (PyModule_AddObject(m, "Taskas", (PyObject *) &TaskasType) < 0) {
        Py_DECREF(&TaskasType);
        Py_DECREF(m);
        return NULL;
    }

    return m;
}

   
