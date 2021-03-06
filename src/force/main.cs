#version 430 core

layout(local_size_x = 1, local_size_y = 1, local_size_z = 1) in;

uniform int dim;
uniform float tdelta;

uniform sampler3D old;
uniform sampler3D fluid;
layout(binding = 0, rgba32f) writeonly uniform image3D new;

void main()
{
  ivec3 index = ivec3(gl_GlobalInvocationID);
  ivec3 center = ivec3(dim/2);
  vec3 diff = vec3(index) - vec3(center);
  vec3 m = 1*vec3(-diff.y/(diff.x*diff.x + diff.y*diff.y + 0.1), diff.x/(diff.x*diff.x + diff.y*diff.y + 0.1), 1);

  imageStore(new, index, vec4(texelFetch(old, index, 0).xyz + m, 0));
}
