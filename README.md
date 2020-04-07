# What is this?
Sartox OS is an operating system made in C# with the COSMOS kit. Its main goal is to provide a minimal but working operating system for any purpose. Currently, it doesn't have that much features, but they will come soon.

# Can I download it?
Yes, you can. Just head to the [releases page](https://github.com/AnErrupTion/Sartox-OS/releases), download the latest release, and boom!

# Trying it
To try it, you'll have to use either VMware or Hyper-V, mainly because the virtual FS is currently not supported on other softwares such as VirtualBox.<br/>
We'll use VMware here as it's easier.<br/>
First, create a new "Typical" VM, make a blank hard drive, set the guest OS to "Other" and version to "Other" too, then continue the process as you want.<br/>
And now, after you've finished creating your VM, head over to your Documents' folder and to "Virtual Machines", get inside the folder of your newly created VM and replace the filesystem (.vmdk file) with [this one](https://github.com/CosmosOS/Cosmos/raw/master/Build/VMWare/Workstation/Filesystem.vmdk).<br/>
If you're on Hyper-V, replace the .vhdx file with [this one](https://github.com/CosmosOS/Cosmos/raw/master/Build/HyperV/Filesystem.vhdx).<br/>
And now, just play the virtual machine and you're done! You can now enjoy Sartox OS!

# Compiling it
Compiling it is easy. You'll have to download Visual Studio 2019, and install the .NET development workload. Now, head to Cosmos' releases page, download the User Kit 20190628 and install it. That's it. Now, launch the "Sartox OS.sln" file, wait for the NuGet packages to be restored, and boom! You can now modify Sartox OS as you wish.

# Thanks
Thanks to :
- The [Aura OS project](https://github.com/aura-systems/Aura-Operating-System) for some parts of the project!
