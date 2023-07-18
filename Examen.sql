-- Crear la base de datos BdiExamen
CREATE DATABASE BdiExamen;

-- Utilizar la base de datos reci�n creada
USE BdiExamen;

-- Crear la tabla tblExamen
CREATE TABLE tblExamen (
    IdExamen INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(255) NULL,
    Descripcion VARCHAR(255) NULL
);


CREATE PROCEDURE spAgregar
    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @CodigoRetorno INT;
    DECLARE @DescripcionRetorno VARCHAR(255);
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Intentar insertar el registro en la tabla
        INSERT INTO tblExamen (Nombre, Descripcion)
        VALUES (@Nombre, @Descripcion);

        -- Si la inserci�n fue exitosa, establecer el c�digo de retorno a cero y la descripci�n de �xito
        SET @CodigoRetorno = 0;
        SET @DescripcionRetorno = 'Registro insertado satisfactoriamente';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, establecer el c�digo de retorno con el n�mero del error y la descripci�n del error
        SET @CodigoRetorno = ERROR_NUMBER();
        SET @DescripcionRetorno = ERROR_MESSAGE();

        ROLLBACK TRANSACTION;
    END CATCH

    -- Devolver los valores de salida
    SELECT @CodigoRetorno AS CodigoRetorno, @DescripcionRetorno AS DescripcionRetorno;
END;




CREATE PROCEDURE spActualizar
    @IdExamen INT,
    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255) 
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CodigoRetorno INT;
    DECLARE @DescripcionRetorno VARCHAR(255);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si el registro a actualizar existe
        IF EXISTS (SELECT 1 FROM tblExamen WHERE IdExamen = @IdExamen)
        BEGIN
            -- Actualizar el registro en la tabla
            UPDATE tblExamen
            SET Nombre = @Nombre,
                Descripcion = @Descripcion
            WHERE IdExamen = @IdExamen;

            -- Si la actualizaci�n fue exitosa, establecer el c�digo de retorno a cero y la descripci�n de �xito
            SET @CodigoRetorno = 0;
            SET @DescripcionRetorno = 'Registro actualizado satisfactoriamente';
        END
        ELSE
        BEGIN
            -- Si el registro no existe, establecer el c�digo de retorno con un valor negativo y la descripci�n de error
            SET @CodigoRetorno = -1;
            SET @DescripcionRetorno = 'El registro con el Id ' + CAST(@IdExamen AS VARCHAR(10)) + ' no existe';
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, establecer el c�digo de retorno con el n�mero del error y la descripci�n del error
        SET @CodigoRetorno = ERROR_NUMBER();
        SET @DescripcionRetorno = ERROR_MESSAGE();

        ROLLBACK TRANSACTION;
    END CATCH

    -- Devolver los valores de salida
    SELECT @CodigoRetorno AS CodigoRetorno, @DescripcionRetorno AS DescripcionRetorno;
END;


CREATE PROCEDURE spEliminar
    @IdExamen INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CodigoRetorno INT;
    DECLARE @DescripcionRetorno VARCHAR(255);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si el registro a eliminar existe
        IF EXISTS (SELECT 1 FROM tblExamen WHERE IdExamen = @IdExamen)
        BEGIN
            -- Eliminar el registro de la tabla
            DELETE FROM tblExamen WHERE IdExamen = @IdExamen;

            -- Si la eliminaci�n fue exitosa, establecer el c�digo de retorno a cero y la descripci�n de �xito
            SET @CodigoRetorno = 0;
            SET @DescripcionRetorno = 'Registro eliminado satisfactoriamente';
        END
        ELSE
        BEGIN
            -- Si el registro no existe, establecer el c�digo de retorno con un valor negativo y la descripci�n de error
            SET @CodigoRetorno = -1;
            SET @DescripcionRetorno = 'El registro con IdExamen ' + CAST(@IdExamen AS VARCHAR(10)) + ' no existe';
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, establecer el c�digo de retorno con el n�mero del error y la descripci�n del error
        SET @CodigoRetorno = ERROR_NUMBER();
        SET @DescripcionRetorno = ERROR_MESSAGE();

        ROLLBACK TRANSACTION;
    END CATCH

    -- Devolver los valores de salida
    SELECT @CodigoRetorno AS CodigoRetorno, @DescripcionRetorno AS DescripcionRetorno;
END;

 CREATE PROCEDURE spConsultar
    @Nombre VARCHAR(255) = NULL,
    @Descripcion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Consultar la tabla tblExamen con los par�metros recibidos
    SELECT IdExamen, Nombre, Descripcion
    FROM tblExamen
    WHERE (@Nombre IS NULL OR Nombre = @Nombre)
      AND (@Descripcion IS NULL OR Descripcion = @Descripcion);
END;