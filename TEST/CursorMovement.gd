extends Area2D

@export	var topLeft = Vector2(50, 50)
@export var bottomRight = Vector2(1230, 670) 

func _ready():
	# Set the cursor to be visible
	Input.set_mouse_mode(Input.MOUSE_MODE_HIDDEN)


# Player (Cursor) follows the mouse position
func _process(_delta):
	# Get the mouse position in global coordinates
	var raw_mouse_position = get_global_mouse_position()
	var mouse_position = Vector2(
		clamp(raw_mouse_position.x, topLeft.x, bottomRight.x),
		clamp(raw_mouse_position.y, topLeft.y, bottomRight.y)
	) 
	position = mouse_position
